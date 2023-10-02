using Amazon.CDK;
using cdk.commun;
using cdk.commun.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using Environment = System.Environment;

namespace apigateway_cognito_lambda_dynamodb
{
    sealed class Program
    {
        private const string StackPrefix = "QuickSlot";
        private const string DefaultEnvironment = "Development";
        private static IConfiguration Configuration { get; set; }

        /// <summary>
        /// the Main method 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var app = new App();
            var env = GetCdkEnvironment();
            var environmentName = GetEnvironmentName();
            LoadConfiguration(environmentName);

            var deploySettings = SupportedDeploySettings.GetDeploySettings(env);
            var paths = GetPathsFromConfiguration();

            PublishHelper.DotnetPublish(
                deploySettings.BuildConfiguration,
                projectPath: paths.ProjectPath,
                outputDirectory: paths.OutputDirectory);

            var codePath = PublishHelper.GetPublishUserFunctionCodePath(deploySettings.BuildConfiguration);
            var stackSuffix = app.Node.TryGetContext("StackNameSuffix")?.ToString();
            var stackProps = new CustomStackProps(env, StackPrefix, stackSuffix, codePath);

            new CdkStack(app, stackProps.StackName, stackProps);
            app.Synth();
        }

        private static Amazon.CDK.Environment GetCdkEnvironment() =>
            new Amazon.CDK.Environment
            {
                Account = Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                Region = Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
            };

        private static string GetEnvironmentName() =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? DefaultEnvironment;

        private static void LoadConfiguration(string environmentName)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true);

            Configuration = configurationBuilder.Build();
        }

        private static (string ProjectPath, string OutputDirectory) GetPathsFromConfiguration()
        {
            var projectPath = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), Configuration["Paths:ProjectPath"]));

            var outputDirectory = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), Configuration["Paths:OutputDirectory"]));

            return (projectPath, outputDirectory);
        }
    }
}
