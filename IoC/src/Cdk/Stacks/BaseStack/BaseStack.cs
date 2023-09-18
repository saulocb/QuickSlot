using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.EC2;
using Constructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.IaC.CDK.Stacks.BaseStack
{
    public class BaseStack : Stack
    {
        public BaseStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // Create a VPC with specific configurations
            var vpc = new Vpc(this, "BaseVPC", new VpcProps
            {
                MaxAzs = 3,  // maximum availability zones
                NatGateways = 1, // number of NAT Gateways

                // Subnet configuration
                SubnetConfiguration = new ISubnetConfiguration[]
                {
                    new SubnetConfiguration
                    {
                        SubnetType = SubnetType.PUBLIC,
                        Name = "PublicSubnet",
                    },
                    new SubnetConfiguration
                    {
                        SubnetType = SubnetType.PRIVATE_WITH_EGRESS,
                        Name = "PrivateSubnetForLambda",
                    },
                    new SubnetConfiguration
                    {
                        SubnetType = SubnetType.PRIVATE_WITH_EGRESS,
                        Name = "PrivateSubnetForDynamoDB",
                    }
                }
            });

            // Create Cognito User Pool
            var userPool  =new UserPool(this, "MyUserPool", new UserPoolProps
            {
                // Configure your user pool properties here
                UserPoolName = "QuickSlotSignIn",
                SignInAliases = new SignInAliases
                {
                    Email = true
                }
            });

            // Output VPC Id and UserPool Id, or store in SSM Parameter Store, etc.
            new CfnOutput(this, "VpcIdOutput", new CfnOutputProps
            {
                Value = vpc.VpcId,
                Description = "The VPC Id"
            });

            new CfnOutput(this, "UserPoolIdOutput", new CfnOutputProps
            {
                Value = userPool.UserPoolId,
                Description = "The User Pool Id"
            });
        }
    }
}
