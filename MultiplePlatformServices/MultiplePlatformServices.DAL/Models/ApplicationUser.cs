using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }

        // Navigation Property
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
