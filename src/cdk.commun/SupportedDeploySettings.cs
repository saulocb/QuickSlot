using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdk.commun
{
    public static class SupportedDeploySettings
    {
        private const string DevelopmentAccountId = "947596463206";

        public static DeploySettings GetDeploySettings(Amazon.CDK.Environment env) =>
            (env.Account, env.Region) switch
            {
                (DevelopmentAccountId, Region.EuWest1) => new DeploySettings(
                    AccountId: DevelopmentAccountId,
                    RegionName: Region.EuWest1,
                    CodeStarConnectionArn: "arn:aws:codestar-connections:eu-west-1:947596463206:connection/e4f2bc1b-3cbd-4a03-b3f4-73c5a55bbc61",
                    BuildConfiguration: "Debug",
                    Environment: env
                ),

                _ => throw new ArgumentException($"Unsupported {nameof(env.Account)}=[{env.Account}] or {nameof(env.Region)}=[{env.Region}]")
            };
    }
}
