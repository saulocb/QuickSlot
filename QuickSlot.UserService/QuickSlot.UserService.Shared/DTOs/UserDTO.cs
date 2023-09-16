using QuickSlot.UserService.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Shared.DTOs
{
    public class UserDTO
    {
        public string? Sub { get; set; }
        public string? Email { get; set; }
        public UserTypeDTO UserType { get; set; }
    }
}
