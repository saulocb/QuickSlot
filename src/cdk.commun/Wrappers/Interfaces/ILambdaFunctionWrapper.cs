using Amazon.CDK.AWS.Lambda;


namespace cdk.commun.Wrappers.Interfaces
{
    /// <summary>
    /// the interface to create a mew function
    /// </summary>
    public interface ILambdaFunctionWrapper
    {
        /// <summary>
        /// the function object it self
        /// </summary>
        Function UserFunction { get; }

        /// <summary>
        /// set the propes
        /// </summary>
        CustomStackProps _props { get; }

        /// <summary>
        /// all the env need to set to this funciton 
        /// </summary>
        /// <param name="key">key.</param>
        /// <param name="value">value.</param>
        void AddEnvironmentVariable(string key, string value);
    }
}
