using Amazon.CDK;
using QuickSlot.IaC.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickSlot.IaC.CDK
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var env = EnvironmentHelper.MakeEnv();
            var deploySettings = SupportedDeploySettings.GetDeploySettings(env);

            var app = new App();
            var envname = "dev";

            // Retrieve the dynamic part of the stack name from the context
            var stackNameSuffix = app.Node.TryGetContext("StackNameSuffix")?.ToString() ?? "quickSlot";
            if (string.IsNullOrWhiteSpace(stackNameSuffix))
            {
                throw new Exception("Please specify StackNameSuffix e.g. -c StackNameSuffix=TestStack");
            }

            // Construct the full stack name
            var stackName = $"services-{stackNameSuffix}-{envname}";

            new CdkStack(app, stackName, new StackProps
            {
                Env = env
            });

            app.Synth();
        }

    }
}
