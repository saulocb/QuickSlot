using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdk.commun.Wrappers.Interfaces
{
    public interface IRestApiWrapper
    {
        void AddLambdaFunctionEndpoint(string path, IFunction lambdaFunction);
        void SetAuthorizer(IAuthorizer authorizer);
        RestApi GetRestApi(); // Method to retrieve the underlying RestApi object, if needed
    }

}
