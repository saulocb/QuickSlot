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
using apigateway_cognito_lambda_dynamodb.Staks;
using apigateway_cognito_lambda_dynamodb.Staks.Base;
using cdk.commun;

namespace apigateway_cognito_lambda_dynamodb
{

    public class CdkStack : Stack
    {
        internal CdkStack(Construct scope, string id, CustomStackProps props) : base(scope, id)
        {
             // new BaseStack(this, "BaseStack", props);
            
            var userServiceStack = new UserServiceStack(this, "UserServiceStack", props);
        }
    }
}
