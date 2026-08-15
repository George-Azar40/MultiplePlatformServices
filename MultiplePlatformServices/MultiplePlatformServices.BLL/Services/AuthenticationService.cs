using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IAddressRepository _addressRepository;


        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IConfiguration configuration,
            IAddressRepository addressRepository
            )
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _addressRepository = addressRepository;
        }

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtSettingsKey = _configuration["JWT:Secret"] ?? "TemporaryFallbackSuperSecretKey123456!!!";
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingsKey));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);

            var emailURL = $"https://localhost:7191/api/Account/confirm?token={token}&id={user.Id}";
            await _emailSender.SendEmailAsync(
            user.Email,
            "Confirm Your Email",
            $@"
            <div style='max-width:600px; margin:40px auto; padding:40px; 
                        background:#ffffff; font-family:Arial,sans-serif; 
                        text-align:center; border-radius:12px;'>
        
                <h1 style='color:#2563eb;'>
                    Welcome to Multiple Platform Services, {request.UserName} 👋
                </h1>

                <p style='color:#555; font-size:16px;'>
                    Thank you for creating an account with us!
                </p>

                <p style='color:#555; font-size:16px;'>
                    Please confirm your email address to activate your account.
                </p>

                <a href='{emailURL}'
                   style='display:inline-block; margin-top:20px; 
                          padding:14px 30px; background:#2563eb; 
                          color:white; text-decoration:none; 
                          border-radius:8px; font-weight:bold;'>
                    Confirm My Email
                </a>

                <p style='color:#999; font-size:13px; margin-top:30px;'>
                    If you did not create this account, you can safely ignore this email.
                </p>

            </div>"
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

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Error"
                };
            }
            var result = await _userManager.CheckPasswordAsync(user,request.Password);
            if (!result)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Wrong Email or Password"
                };
            }

            await _emailSender.SendEmailAsync(
            request.Email,
            "Security Alert - New Login Attempt",
            $@"
            <div style='max-width:600px; margin:40px auto; padding:40px; 
                        background:#ffffff; font-family:Arial,sans-serif; 
                        text-align:center; border-radius:12px; 
                        box-shadow:0 4px 15px rgba(0,0,0,0.08);'>

                <h1 style='color:#dc2626;'>
                    Security Alert 🔐
                </h1>

                <p style='color:#333; font-size:18px;'>
                    Someone tried to log in to your account.
                </p>

                <p style='color:#666; font-size:16px; line-height:1.6;'>
                    If this was you, you can safely ignore this email.
                    If you did not try to log in, please reset your password immediately.
                </p>

                <p style='color:#999; font-size:13px; margin-top:30px;'>
                    This is an automated security notification from Multiple Platform Services.
                </p>

            </div>"
             );

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";
            var token = await GenerateJwtTokenAsync(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Login Successfully Done",
                Token = token,
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = role
            };
        }

        public async Task<UserProfileResponse?> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var addresses = await _addressRepository.GetAllAsync(a => a.UserId == userId);

            return new UserProfileResponse
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                City = user.City,
                Street = user.Street,
                Addresses = addresses.Adapt<List<AddressResponse>>(),
                Roles = roles.ToList()
            };
        }

        public async Task<bool> UpdateUserProfileAsync(string userId, UpdateProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.City = request.City;
            user.Street = request.Street;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
