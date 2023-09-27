using Amazon.CDK;
using cdk.commun;
using cdk.commun.Helpers;

namespace apigateway_cognito_lambda_dynamodb
{
    sealed class Program
    {
        private const string StackPrefix = "QuickSlot";
        public static void Main(string[] args)
        {
            var app = new App();

            var stackSuffix = app.Node.TryGetContext("StackNameSuffix")?.ToString();
            //if (string.IsNullOrWhiteSpace(stackSuffix))
            //{
            //    throw new Exception("Please specify StackNameSuffix e.g. -c StackNameSuffix=TestStack");
            //}

            var env = new Amazon.CDK.Environment
            {
                Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
            };

            var stackProps = new CustomStackProps(env, StackPrefix, stackSuffix);

            var deploySettings = SupportedDeploySettings.GetDeploySettings(env);

            PublishHelper.DotnetPublish(deploySettings.BuildConfiguration, projectPath: "../../Runtime");
            string publishCodePath = PublishHelper.GetPublishUserFunctionCodePath(deploySettings.BuildConfiguration);

            new CdkStack(app, stackProps.StackName, stackProps);

            app.Synth();
        }
    }
}
