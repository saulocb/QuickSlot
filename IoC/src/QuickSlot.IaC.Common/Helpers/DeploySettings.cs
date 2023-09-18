using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.IaC.Common.Helpers
{
    public record DeploySettings(
      string AccountId,
      string RegionName,
      string CodeStarConnectionArn,
      string BuildConfiguration,
      Amazon.CDK.Environment Environment
  );
}
