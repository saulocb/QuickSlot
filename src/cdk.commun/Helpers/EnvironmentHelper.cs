using Environment = Amazon.CDK.Environment;

namespace cdk.commun.Helpers;

public static class EnvironmentHelper
{
    public static Environment MakeEnv() =>
        new Environment
        {
            Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
            Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION")
        };
}