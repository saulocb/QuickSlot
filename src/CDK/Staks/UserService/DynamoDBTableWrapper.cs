using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using cdk.commun;
using cdk.commun.Wrappers.Interfaces;
using Constructs;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;

namespace apigateway_cognito_lambda_dynamodb.Staks.UserService
{
    internal class DynamoDBTableWrapper : Construct,  IDynamoDBTableWrapper
    {
        public ITable UserServiceTable { get; private set; }
        public CustomStackProps _props { get; private set; }

        public DynamoDBTableWrapper(Construct scope, string id, CustomStackProps props) : base(scope, id)
        {
            _props = props;
            this.UserServiceTable = CreateProductTable(_props);
        }

        public void GrantRead(Function function)
        {
            UserServiceTable.GrantReadData(function);
        }

        private Table CreateProductTable(CustomStackProps props)
        {
            var dynamoDbTable = new Table(this, $"{props.StackName}-{TableNames.UserServiceTableName}", new TableProps()
            {
                BillingMode = BillingMode.PAY_PER_REQUEST,
                TableName = TableNames.UserServiceTableName,
                PartitionKey = new Attribute()
                {
                    Name = "PK",
                    Type = AttributeType.STRING
                },
                SortKey = new Attribute()
                {
                    Name = "SK",
                    Type = AttributeType.STRING
                },
                RemovalPolicy =  Amazon.CDK.RemovalPolicy.DESTROY

            });
            return dynamoDbTable;
        }

    }
}
