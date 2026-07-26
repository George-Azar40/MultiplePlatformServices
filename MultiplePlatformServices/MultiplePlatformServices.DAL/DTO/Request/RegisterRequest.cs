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
        public String UserName { get; set; }
        public String Email { get; set; }
        public UserRole Role { get; set; }
        public String Password { get; set; }
        public String PhoneNumber { get; set; }
        public String FullName { get; set; }
    }
}
