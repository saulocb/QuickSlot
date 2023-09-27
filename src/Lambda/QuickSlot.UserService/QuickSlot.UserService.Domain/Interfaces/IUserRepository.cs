using QuickSlot.UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
