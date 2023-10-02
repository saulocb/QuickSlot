using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using cdk.commun;
using cdk.commun.Wrappers.Interfaces;
using Constructs;


namespace apigateway_cognito_lambda_dynamodb.Staks.UserService
{
    public class LambdaFunctionWrapper : Construct,  ILambdaFunctionWrapper
    {
        public CustomStackProps _props { get; private set; }

        public Function UserFunction { get; private set; }

        public LambdaFunctionWrapper(Construct scope, string id, CustomStackProps props) : base(scope, id)
        {
            _props = props ?? throw new ArgumentNullException(nameof(props));

            // create function
            this.UserFunction =  CreateUserFunction();

        }

        private Function CreateUserFunction()
        {
            var environment = new Dictionary<string, string>
            {
                {"REGION", _props.Env.Region},
                {"PRIMARY_KEY",  "PK"},
                {"SortKey_KEY", "SK"},
                {"TABLE_NAME", TableNames.UserServiceTableName},
            };

            this.UserFunction = new Function(this, $"UserFunction-{_props.StackName}", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Handler = "QuickSlot.UserService.Lambda.Api::QuickSlot.UserService.Lambda.Api.Function::FunctionHandler",
                Code = Code.FromAsset("./dist/UserFunction"),
                Environment = environment,
                Timeout = Duration.Minutes(1),
                MemorySize = 256,
            });
            return this.UserFunction;
        }

        public void AddEnvironmentVariable(string key, string value)
        {
            UserFunction.AddEnvironment(key, value);
        }
    }
}
