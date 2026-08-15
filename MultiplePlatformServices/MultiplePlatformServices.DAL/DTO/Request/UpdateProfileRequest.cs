using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Request
{
    public class UpdateProfileRequest
    {
        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }
    }
}
