using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Utilities
{
    public class RoleSeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task DataSeed()
        {
            string []roles = ["Admin" , "User" , "Vendor" , "Freelancer"];
            if(!await _roleManager.Roles.AnyAsync())
            {
                foreach(var role in roles)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
