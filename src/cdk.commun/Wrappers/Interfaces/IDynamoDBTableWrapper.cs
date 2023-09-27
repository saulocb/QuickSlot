using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;

namespace cdk.commun.Wrappers.Interfaces
{
    /// <summary>
    /// the interface to create a new dynamodb table
    /// </summary>
    public interface IDynamoDBTableWrapper
    {
        /// <summary>
        /// the table object 
        /// </summary>
        ITable? UserServiceTable { get; }

        /// <summary>
        /// set the propes
        /// </summary>
        CustomStackProps _props { get; }

        /// <summary>
        /// to give function access to this table.
        /// </summary>
        /// <param name="function">the lmbda funciton that will get access to this table.</param>
        void GrantRead(Function function);
    }
}
