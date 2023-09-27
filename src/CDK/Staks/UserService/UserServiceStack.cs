using Amazon.CDK;
using cdk.commun;
using cdk.commun.Wrappers.Interfaces;
using Constructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apigateway_cognito_lambda_dynamodb.Staks.UserService
{
    internal class UserServiceStack : Construct
    {
        public IDynamoDBTableWrapper? UserTable { get; private set; }
        public ILambdaFunctionWrapper? UserFunction { get; private set; }

        public UserServiceStack(Construct scope, string id, CustomStackProps props) : base(scope, id)
        {
             UserTable = new DynamoDBTableWrapper(this, "UserServiceTableStack", props);
             UserFunction = new LambdaFunctionWrapper(this, "FunctionStack", props);

             UserTable.GrantRead(UserFunction.UserFunction);
        }
    }
}
