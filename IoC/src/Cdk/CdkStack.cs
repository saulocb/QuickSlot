using Amazon.CDK;
using Constructs;
using QuickSlot.IaC.CDK.Stacks.BaseStack;

namespace QuickSlot.IaC.CDK
{
    public class CdkStack : Stack
    {
        internal CdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            new BaseStack(this, "BaseStack");
        }
    }
}
