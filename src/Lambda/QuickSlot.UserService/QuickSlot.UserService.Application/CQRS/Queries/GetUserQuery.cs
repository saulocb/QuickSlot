using MediatR;
using QuickSlot.UserService.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.CQRS.Queries
{
    public class GetUserQuery : IRequest<UserDTO> 
    {
        public int Id { get; }

        public GetUserQuery(int id)
        {
            Id = id;
        }
    }
}
