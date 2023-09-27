using QuickSlot.UserService.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Application.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO dto);
        UserDTO GetUser(int id);
        List<UserDTO> GetAllUsers();
        void UpdateUser(int id, UserDTO dto);
        void DeleteUser(int id);
    }
}
