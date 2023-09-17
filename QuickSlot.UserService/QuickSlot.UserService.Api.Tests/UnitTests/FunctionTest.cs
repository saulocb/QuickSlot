using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using QuickSlot.UserService.Api;
using Amazon.Lambda.APIGatewayEvents;
using MediatR;
using Moq;
using QuickSlot.UserService.Application.CQRS.Commands;
using QuickSlot.UserService.Shared.DTOs;
using QuickSlot.UserService.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using QuickSlot.UserService.Shared.DTOs.Enums;

namespace QuickSlot.UserService.Tests.UnitTests;

public class FunctionTest
{
    private readonly Mock<IMediator> _mediatorMock;

    public FunctionTest()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    //[Fact]
    //public async Task TestFunctionHandler()
    //{
    //    // Arrange
    //    var function = new Function();
    //    var context = new TestLambdaContext();
    //    var request = new APIGatewayProxyRequest();  
    //    var expectedResponse = new APIGatewayProxyResponse();  

    //    // Act
    //    var response = await function.FunctionHandler(request, context);

    //    // Assert
    //    Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
    //    // Add more assertions as necessary, such as comparing headers, body etc.
    //}

    [Fact]
    public async Task TestCreateUserEndpoint()
    {
        // Arrange
        var command = new CreateUserCommand("Sub1", "email@example.com", UserTypeDTO.Customer);
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("UserId123");

        var controller = new UserController(_mediatorMock.Object);

        // Act
        var result = await controller.Create(command);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task TestUpdateUserAddressEndpoint()
    {
        // Arrange
        var command = new UpdateUserAddressCommand { PK = "PK1", NewAddress = new AddressDTO() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserAddressCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var controller = new UserController(_mediatorMock.Object);

        // Act
        var result = await controller.UpdateAddress(command);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task TestUpdateUserContactEndpoint()
    {
        // Arrange
        var command = new UpdateUserContactCommand { PK = "PK1", NewContact = new ContactDTO() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserContactCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var controller = new UserController(_mediatorMock.Object);

        // Act
        var result = await controller.UpdateContact(command); 

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task TestUpdateUserBillPaymentMethodEndpoint()
    {
        // Arrange
        var command = new UpdateUserBillPaymentMethodCommand { PK = "PK1", NewBillPaymentMethod = new BillPaymentMethodDTO() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserBillPaymentMethodCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var controller = new UserController(_mediatorMock.Object);

        // Act
        var result = await controller.UpdateBillPaymentMethod(command);  

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }
}
