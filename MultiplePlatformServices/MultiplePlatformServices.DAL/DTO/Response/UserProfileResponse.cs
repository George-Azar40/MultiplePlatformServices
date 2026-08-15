using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class UserProfileResponse
    {
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string FullName { get; set; } = null!;

        public string? City { get; set; }

        public string? Street { get; set; }

        public List<AddressResponse> Addresses { get; set; } = new List<AddressResponse>();

        public List<string> Roles { get; set; } = new List<string>();
    }
}
