using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using QuickSlot.UserService.Api;
using Amazon.Lambda.APIGatewayEvents;

namespace QuickSlot.UserService.Tests.UnitTests;

public class FunctionTest
{
    [Fact]
    public async Task TestFunctionHandler()
    {
        // Arrange
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new APIGatewayProxyRequest();  // You can populate this as needed for your test
        var expectedResponse = new APIGatewayProxyResponse();  // Populate this with expected values for your test

        // Act
        var response = await function.FunctionHandler(request, context);

        // Assert
        Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
        // Add more assertions as necessary, such as comparing headers, body etc.
    }
}
