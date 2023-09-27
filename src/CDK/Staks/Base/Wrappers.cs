using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using cdk.commun.Wrappers.Interfaces;
using Constructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apigateway_cognito_lambda_dynamodb.Staks.Base
{
    public class RestApiWrapper : IRestApiWrapper
    {
        private readonly RestApi _apiGateway;

        public RestApiWrapper(Construct construct, string id, TokenAuthorizer auth)
        {
            _apiGateway = new RestApi(construct, "SampleApi", new RestApiProps()
            {
                RestApiName = "SampleApi",
                Description = "Lambda Backed API",
                DeployOptions = new StageOptions
                {
                    StageName = "Dev",
                    ThrottlingBurstLimit = 10,
                    ThrottlingRateLimit = 10,
                    LoggingLevel = MethodLoggingLevel.INFO,
                    MetricsEnabled = true
                },
                DefaultMethodOptions = new MethodOptions
                {
                    Authorizer = auth,
                    AuthorizationType = AuthorizationType.CUSTOM
                },

            });
        }

        public void AddLambdaFunctionEndpoint(string path, IFunction lambdaFunction)
        {
            var endpoint = _apiGateway.Root.AddResource(path);
            endpoint.AddMethod("GET", new LambdaIntegration(lambdaFunction));
        }

        public void SetAuthorizer(IAuthorizer authorizer)
        {
            // set the authorizer for your API Gateway
        }

        public RestApi GetRestApi()
        {
            return _apiGateway;
        }
    }
}
