using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdk.commun
{
    public record DeploySettings(
        string AccountId,
        string RegionName,
        string CodeStarConnectionArn,
        string BuildConfiguration,
        Amazon.CDK.Environment Environment
    );
}
