using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickSlot.UserService.Application.CQRS.Commands;


namespace QuickSlot.UserService.Api.Controllers  
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserCommand command)
        {
            try
            {
                var userId = await _mediator.Send(command);
                return Ok(userId);
            }
            catch (ValidationException ve)
            {
                return BadRequest(new { Errors = ve.Errors });
            }
        }

        [HttpPut("address")]
        public async Task<ActionResult<bool>> UpdateAddress([FromBody] UpdateUserAddressCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException ve)
            {
                return BadRequest(new { Errors = ve.Errors });
            }
        }

        [HttpPut("billpaymentmethod")]
        public async Task<ActionResult<bool>> UpdateBillPaymentMethod([FromBody] UpdateUserBillPaymentMethodCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException ve)
            {
                return BadRequest(new { Errors = ve.Errors });
            }
        }

        [HttpPut("contact")]
        public async Task<ActionResult<bool>> UpdateContact([FromBody] UpdateUserContactCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException ve)
            {
                return BadRequest(new { Errors = ve.Errors });
            }
        }
    }

}
