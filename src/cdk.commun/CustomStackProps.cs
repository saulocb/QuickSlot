using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using cdk.commun.Wrappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environment = Amazon.CDK.Environment;

namespace cdk.commun
{
    public class CustomStackProps : StackProps
    {
        public CustomStackProps(
            Environment env,
            string stackPrefix,
            string stackSuffix
          , string codePath)
        {
            Env = env;
            StackPrefix = stackPrefix;
            StackSuffix = stackSuffix;

            StackName = $"{stackPrefix}-{stackSuffix}";
            Description = $"{stackPrefix} {stackSuffix}";
            CodePath=codePath;
        }

        public IRestApiWrapper? RestApiWrapper { get; }
        public string StackPrefix { get; }
        public string StackSuffix { get; }
        public string CodePath { get; set; }
    }

}
