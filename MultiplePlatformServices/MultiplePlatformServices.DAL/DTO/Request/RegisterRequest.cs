using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Request
{
    public enum UserRole
    {
        //Admin = 1, the user can not choose admin by himself !!!
        User = 2,
        Vendor = 3,
        Freelancer = 4,
    }
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
    }
}
