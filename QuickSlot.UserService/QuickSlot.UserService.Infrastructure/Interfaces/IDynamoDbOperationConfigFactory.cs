using Amazon.DynamoDBv2.DataModel;


namespace QuickSlot.UserService.Infrastructure.Interfaces
{
    public interface IDynamoDbOperationConfigFactory
    {
        DynamoDBOperationConfig CreateConfigFor(string tableName);
    }

}
