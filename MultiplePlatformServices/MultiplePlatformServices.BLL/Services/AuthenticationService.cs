using Mapster;
using Microsoft.AspNetCore.Identity;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;


        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {

            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponse { Success = false , Message = "Error" };
            }

            await _userManager.AddToRoleAsync(user, request.Role.ToString());

            //make an endpoint into the controller then get its URL ... var emailURL = ""
            await _emailSender.SendEmailAsync(
                user.Email,
                "Welcome",
                $"<h1>Welcome {request.UserName}</h1>" + """<a href="">confirm</a>"""
                );
            return null;
        }
    }
}
