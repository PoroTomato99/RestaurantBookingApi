using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantBookingApi.Models;
using RestaurantBookingApi.RestaurantData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace RestaurantBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
            private readonly UserManager<ApplicationUser> userManager;
            private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IConfiguration configuration,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender, ILogger<AuthenticationController> logger)
        {
            _configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Authentication Controller");

        }

        //POST : Authentication/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                if (await userManager.GetLockoutEndDateAsync(user) > DateTime.Now)
                {
                    return BadRequest(new Response { Type = "Failed to Login", Message = "Account is Locked Please Contact Admin at 011-111111" });
                }

                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    return BadRequest(new Response { Type = $"Failed", Message = $"Unable to login unverified account" });
                }
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    //expiration = token.ValidTo
                });
            }
            _logger.LogInformation($"Login Error, Invalid Username or Password");
            return Unauthorized(new Response { Type = "Error", Message = "Invaid Username or Password" });
        }


        //POST : register new user
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                _logger.LogInformation($"Existed Username being used for User Createion");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User already existed!" });
            }

            var emailExist = await userManager.FindByEmailAsync(model.Email);
            if (emailExist != null)
            {
                _logger.LogInformation($"Existed Email being used for User Creation");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User already existed." });
            }


            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            try
            {
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning($"{StatusCodes.Status500InternalServerError}:User Creation Failed");
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User creation failed! Please check user details and try again." });
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Master))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Master));
                if (await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.User);
                }
                //send account confirmation email 
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var baseUrl = _configuration.GetValue<string>("RestaurantUI_Url");
                var link = $"<a href='{baseUrl}authenticate/confirmEmail?token={token}&email={user.Email}'> Click Here </a>";
                var message = new Message(new string[] { user.Email }, "Account Confirmation", $"Please Click this link to confirm your account : {link}");
                _emailSender.SendEmail(message);
                return Ok(new Response { Type = "Success", Message = $"User {user.UserName}, successfully created." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Register user exception error : {ex.Message}");
                return BadRequest(new Response { Type = $"Error", Message = $"{ex.Message}" });
            }
        }


        //confirm email 
        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel confirmEmail)
        {
            var user = await userManager.FindByEmailAsync(confirmEmail.Email);
            if (user == null)
            {
                return NotFound(new Response { Type = $"{StatusCodes.Status404NotFound}", Message = $"No user found." });
            }
            try
            {
                //confirmEmail.Token = HttpUtility.UrlDecode(confirmEmail.Token);
                confirmEmail.Token = confirmEmail.Token.Replace(' ', '+');
                var verifiedAccount = await userManager.ConfirmEmailAsync(user, confirmEmail.Token);
                if (!verifiedAccount.Succeeded)
                {
                    _logger.LogWarning($"{StatusCodes.Status500InternalServerError}, failed to verified account");
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = $"Error", Message = $"Failed to Verified Account" });
                }
                return Ok(new Response { Type = $"{StatusCodes.Status200OK}", Message = $"Successfully Verified Account" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Confirm email exception error : {ex.Message}");
                return BadRequest(new Response { Type = $"Error", Message = $"{ex.Message}" });
                throw;
            }
        }

        //Register Admin 
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                _logger.LogInformation($"Existed Username is used for User Creation");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User already existed!" });
            }

            var emailExist = await userManager.FindByEmailAsync(model.Email);
            if (emailExist != null)
            {
                _logger.LogInformation($"Existed Email being used for User Creation");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User already existed." });
            }


            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            try
            {
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning($"{StatusCodes.Status500InternalServerError}:User Creation Failed");
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User creation failed! Please check user details and try again." });
                }


                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Master))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Master));

                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }



                //send account confirmation email 
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var baseUrl = _configuration.GetValue<string>("RestaurantUI_Url");
                var link = $"<a href='{baseUrl}authenticate/confirmEmail?token={token}&email={user.Email}'> Click Here </a> <br /> " +
                    $"<h4> Username :{user.UserName}</h4> <br />" +
                    $"<h4> Temporary Password : NewAdmin@99 </h4>";
                var message = new Message(new string[] { user.Email }, "Account Confirmation", $"Please Click this link to confirm your account : {link}");
                _emailSender.SendEmail(message);
                return Ok(new Response { Type = "Success", Message = "Admin created successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Register admin exception error : {ex.Message}");
                return BadRequest(new Response { Type = $"Error", Message = $"{ex.Message}" });
                throw;
            }
        }


        //Register Restaurant Owner
        [HttpPost]
        [Route("register-master")]
        public async Task<IActionResult> RegisterSeller([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                _logger.LogInformation($"Existed Username is used for User Creation");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User already exists!" });
            }
            var emailExist = await userManager.FindByEmailAsync(model.Email);
            if (emailExist != null)
            {
                _logger.LogInformation($"Existed email is used for User Creation");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = $"Error", Message = $"Email Already Registered" });
            }


            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                _logger.LogWarning($"{StatusCodes.Status500InternalServerError}:User Creation Failed");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Master))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Master));
            if (await roleManager.RoleExistsAsync(UserRoles.Master))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Master);
            }
            //send account confirmation email 
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var baseUrl = _configuration.GetValue<string>("RestaurantUI_Url");
            var link = $"<a href='{baseUrl}authenticate/confirmEmail?token={token}&email={user.Email}'> Click Here </a>";
            var message = new Message(new string[] { user.Email }, "Account Confirmation", $"Please Click this link to confirm your account : {link}");
            _emailSender.SendEmail(message);
            return Ok(new Response { Type = "Sucess", Message = "Master Admin successfullt created!" });
        }


        //Forget password
        [HttpPost]
        [Route("forget-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotpassword)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(forgotpassword.Email);
            }


            var user = await userManager.FindByEmailAsync(forgotpassword.Email);
            if (user == null)
            {
                _logger.LogInformation("User not Found baed on email provided");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"This email is not registered"
                });
            }


            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var baseUrl = _configuration.GetValue<string>("ApiConsumerBaseUrl");
            var link = $"<a href='{baseUrl}authenticate/ResetPassword?token={token}&email={user.Email}'> Click Here </a>";
            try
            {
                var message = new Message(new string[]
                {
                    //"porotomato@gmail.com"
                    user.Email
                },
                "Ecommerce Reset Password",
                $"Hi, {user.UserName},<br />" +
                $"This is the Link to Reset Your Password : {link}");
                _emailSender.SendEmail(message);
                return Ok(new Response
                {
                    Type = $"User Found : {user.UserName}",
                    Message = $"Reset Password Link Has Been Sent to Email. {token}"
                });
            }
            catch (Exception ex)
            {
                //log exception error
                _logger.LogError($"Send Email Exception Error Occured : {ex.Message}");

                //return the exception error to cosumer
                return BadRequest(new Response
                {
                    Type = $"{StatusCodes.Status400BadRequest}",
                    Message = $"Exception Error Occurred : {ex.Message}"
                });
                throw;
            }
        }


        //reset password
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPassword)
        {

            if (!ModelState.IsValid)
            {
                return NotFound(new Response { Type = $"Error", Message = $"Model State Invalid" });
            }
            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No User Found"
                });
            }
            try
            {
                resetPassword.Token = HttpUtility.UrlDecode(resetPassword.Token);
                resetPassword.Token = resetPassword.Token.Replace(' ', '+');
                var changePassword = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!changePassword.Succeeded)
                {
                    //log error
                    return NotFound(new Response
                    {
                        Type = $"Error",
                        Message = $"Failed to Reset Password. Token : {resetPassword.Token}"
                    });

                }
                return Ok(new Response
                {
                    Type = $"{StatusCodes.Status200OK}",
                    Message = $"Succesfully Reset Password"
                });
            }
            catch (Exception ex)
            {
                //log exception error 

                //return exception errror
                return BadRequest(new Response
                {
                    Type = $"Exception Error ({StatusCodes.Status400BadRequest})",
                    Message = $"{ex.Message}"
                });
            }
        }



        [HttpPost]
        [Route("lock_user")]
        public async Task<IActionResult> LockUser([FromBody] LockUserModel lockUser)
        {
            var user = await userManager.FindByIdAsync(lockUser.UserId);
            if (user == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound} : No User with userid of {lockUser.UserId}");
                return NotFound(new Response
                {
                    Type = $"{StatusCodes.Status404NotFound}",
                    Message = $"No User Found"
                });
            }

            try
            {
                if (lockUser.LockoutEnd == "Lock")
                {
                    var lockSuccess = await LockUserAsync(user.Id);
                    if (!lockSuccess)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "Failed to Lock User" });
                    }
                    var getLockedUser = await userManager.FindByNameAsync(user.UserName);
                    return Ok(new LockUserModel { UserId = getLockedUser.Id, LockoutEnd = getLockedUser.LockoutEnd > DateTime.Now ? "Locked" : "Unlocked" });
                }
                else
                {
                    var unlockSuccess = await UnlockUserAsync(user.Id);
                    if (!unlockSuccess)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Type = "Error", Message = "Failed to Unlock User" });
                    }
                    var getLockedUser = await userManager.FindByNameAsync(user.UserName);
                    return Ok(new LockUserModel { UserId = getLockedUser.Id, LockoutEnd = getLockedUser.LockoutEnd > DateTime.Now ? "Locked" : "Unlocked" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{StatusCodes.Status400BadRequest}, Exception error occurred : {ex.Message}");

                return BadRequest(new Response
                {
                    Type = $"Exception Error ({StatusCodes.Status400BadRequest})",
                    Message = $"{ex.Message}"
                });
            }
        }

        [NonAction]
        public async Task<bool> LockUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, User Not Found!");
                return false;
            }

            var EnableLockOut = await userManager.SetLockoutEnabledAsync(user, true);
            if (EnableLockOut.Succeeded)
            {
                var Errors = new IdentityError { Code = $"Error", Description = $"Failed to enable locout end." };
                _logger.LogWarning($"{StatusCodes.Status500InternalServerError}, Error Enable LockOut Feature => {Errors.Code} : {Errors.Description}");
                return false;
            }

            var lockoutUser = await userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(100));
            if (!lockoutUser.Succeeded)
            {
                _logger.LogWarning($"{StatusCodes.Status500InternalServerError}, Error Lockout User");
                return false;
                //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Error Lockout User" });
            }
            return true;
        }

        [NonAction]
        public async Task<bool> UnlockUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"{StatusCodes.Status404NotFound}, User Not Found!");
                return false;
            }

            var unlockUser = await userManager.SetLockoutEndDateAsync(user, DateTime.Now - TimeSpan.FromMinutes(1));
            if (!unlockUser.Succeeded)
            {
                _logger.LogWarning($"{StatusCodes.Status500InternalServerError}, Error Unlock User");
                return false;
            }
            return true;
        }
    }
}
