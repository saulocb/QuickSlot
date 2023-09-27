using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Constructs;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.DynamoDB;
using System.Web;
using System.Collections.Generic;
using System;
using AssetOptions = Amazon.CDK.AWS.S3.Assets.AssetOptions;
using apigateway_cognito_lambda_dynamodb.Staks.UserService;
using cdk.commun.Wrappers.Interfaces;
using cdk.commun;

namespace apigateway_cognito_lambda_dynamodb.Staks.Base
{
    public class BaseStack : Construct
    {
        public IRestApiWrapper? _apiGateway { get; private set; }
        internal BaseStack(Construct scope, string id, CustomStackProps props) : base(scope, id)
        {
            Console.WriteLine("BaseStack constructor called");

            #region "Cognito User Pool"
                    // Create Cognito User Pool with Email Sign-in.
                    var userPool = new UserPool(this, "CognitoUserPool", new UserPoolProps
            {
                UserPoolName = $"CognitoUserPool-{props.StackName}",
                RemovalPolicy = RemovalPolicy.DESTROY, //Delete Cognito User pool on CDK destroy
                SignInAliases = new SignInAliases
                {
                    Email = true
                },
                SelfSignUpEnabled = false
            });

            //Create Cognito user group
            var userPoolGroupReadOnly = new CfnUserPoolGroup(this, $"UserPoolGroupReadOnly-{props.StackName}", new CfnUserPoolGroupProps
            {
                UserPoolId = userPool.UserPoolId,
                Description = "Read Only Access",
                GroupName = "read-only",
            });

            //Create Cognito user group
            var userPoolGroupReadUpdateAdd = new CfnUserPoolGroup(this, $"UserPoolGroupReadUpdateAdd-{props.StackName}", new CfnUserPoolGroupProps
            {
                UserPoolId = userPool.UserPoolId,
                Description = "Full Access",
                GroupName = "read-update-add",
            });

            // Create Cognito App Client
            string CallbackUrl = "http://localhost";
            var cognitoAppClient = new UserPoolClient(this, $"CognitoAppClient-{props.StackName}", new UserPoolClientProps
            {
                UserPoolClientName = $"CognitoAppClient-{props.StackName}",
                UserPool = userPool,
                AccessTokenValidity = Duration.Minutes(20),
                IdTokenValidity = Duration.Minutes(20),
                RefreshTokenValidity = Duration.Hours(1),
                AuthFlows = new AuthFlow
                {
                    UserPassword = true,
                    UserSrp = true
                },
                OAuth = new OAuthSettings
                {
                    CallbackUrls = new string[] { CallbackUrl },
                    Flows = new OAuthFlows
                    {
                        ImplicitCodeGrant = true
                    },
                    Scopes = new[] { OAuthScope.EMAIL, OAuthScope.OPENID }
                }
            });

            //Create Domain name for Cognito Hosted UI
            var cognitoDomainName = new UserPoolDomain(this, $"CognitoDomainName-{props.StackName}", new UserPoolDomainProps
            {
                UserPool = userPool,
                CognitoDomain = new CognitoDomainOptions
                {
                    DomainPrefix = "sign-in-" + cognitoAppClient.UserPoolClientId
                }
            });

            #endregion

            #region "Auth Lambda and DynamoDB table"
            // DynamoDB table
            var userPoolGroupApiPolicyTable = new Table(this, $"UserPoolGroupApiPolicy-{props.StackName}", new TableProps
            {
                TableName = "UserGroupApiGwAccessPolicy",
                RemovalPolicy = RemovalPolicy.DESTROY, //Delete DynamoDB table on CDK destroy
                PartitionKey = new Amazon.CDK.AWS.DynamoDB.Attribute { Name = "UserPoolGroup", Type = AttributeType.STRING }
            });

            var authLambdaEnvVariables = new Dictionary<string, string>
            {
                {"REGION", props.Env.Region},
                {"COGNITO_USER_POOL_ID", userPool.UserPoolId},
                {"CLIENT_ID", cognitoAppClient.UserPoolClientId},
                {"TABLE_NAME", userPoolGroupApiPolicyTable.TableName},
            };

            // Auth Lambda function
            var authLambdaFun = new Function(this, $"AuthLambdaFunc-{props.StackName}", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Handler = "AuthFunction::Lambda.AuthFunction.Function::FunctionHandler",
                Code = Code.FromAsset("./dist/AuthFunction"),
                Environment = authLambdaEnvVariables,
                Timeout = Duration.Minutes(1),
                MemorySize = 256
            });
            userPoolGroupApiPolicyTable.GrantReadData(authLambdaFun);

            #endregion

            #region "API Gateway and backend Lambda function"
            // Lambda function
            var backendLambdaFun = new Function(this, $"BackendLambdaFunc-{props.StackName}", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Handler = "BackendFunction::Lambda.BackendFunction.Function::FunctionHandler",
                Code = Code.FromAsset("./dist/BackendFunction"),
            });

            var tokenAuthorizer = new TokenAuthorizer(this, $"LambdaTokenAuthorizer-{props.StackName}", new TokenAuthorizerProps
            {
                Handler = authLambdaFun,
                IdentitySource = "method.request.header.authorization",
                ResultsCacheTtl = Duration.Seconds(0)
            });

            // APIGateway 
            _apiGateway = new RestApiWrapper(this, $"Api-{props.StackName}", tokenAuthorizer);
           // props.RestApiWrapper = _apiGateway;
            var apiGateway = _apiGateway?.GetRestApi();


            // APIGateway - GET endpoint
            var getEndpoint = apiGateway.Root.AddMethod("GET", new LambdaIntegration(backendLambdaFun, new LambdaIntegrationOptions
            {
                Proxy = true,
            }));

            // APIGateway - POST endpoint
            var postEndpoint = apiGateway.Root.AddMethod("POST", new LambdaIntegration(backendLambdaFun, new LambdaIntegrationOptions
            {
                Proxy = true
            }));
            #endregion

            #region "CloudFormation Output"
            new CfnOutput(this, $"CognitoHostedUIUrl-{props.StackName}", new CfnOutputProps
            {
                Value = string.Format("{0}/login?response_type=token&client_id={1}&redirect_uri={2}", cognitoDomainName.BaseUrl(), cognitoAppClient.UserPoolClientId, HttpUtility.UrlEncode(CallbackUrl))
            });

            new CfnOutput(this, $"APIGWEndpoint-{props.StackName}", new CfnOutputProps
            {
                Value = apiGateway.Url
            });

            #endregion
        }
    }
}
