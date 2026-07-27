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
                return new RegisterResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            await _userManager.AddToRoleAsync(user, request.Role.ToString());

            //make an endpoint into the controller then get its URL ... var emailURL = ""
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);

            var emailURL = $"https://localhost:7191/api/Account/confirm?token={token}&id={user.Id}";
            await _emailSender.SendEmailAsync(
                user.Email,
                "Welcome",
                $"<h1>Welcome {request.UserName}</h1>" + $"""<a href="{emailURL}">confirm</a>"""
                );

            return new RegisterResponse { Success = true, Message = "Register Success" };
        }


        public async Task<bool> confirmEmailAsync(string token, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user,token);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

    }
}
