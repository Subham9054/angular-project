using Login.Model.Entities.LoginEntity;
using Login.Repository.Repositories.Interfaces;
using Login.Repository.Repositories.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using Microsoft.Extensions.Configuration;
using Login.Core.Helper;
using Newtonsoft.Json;

namespace Login.API.Controllers
{
    public class LoginController : Controller
    {

        public IConfiguration _configuration;
        private readonly ILoginRepository _loginRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public LoginController(IConfiguration configuration, ILoginRepository ILoginRepository, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _loginRepository = ILoginRepository;

            _hostingEnvironment = hostingEnvironment;
        }

        //[HttpGet("Checklogindetails")]

        //public async Task<IActionResult> Checklogindetails(LoginEntity Id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var message = string.Join(" | ", ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage));
        //        return Ok(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
        //    }
        //    else
        //    {

        //        List<LoginEntity> lst = await _ILoginRepository.login(Id);
        //        var jsonres = JsonConvert.SerializeObject(lst?.FirstOrDefault());
        //        return Ok(jsonres);
        //    }

        //}

        private async Task<Users> Authenticatedusers(Users user)
        {
            user.vchPassWord = Md5Encryption.MD5Encryption(user.vchPassWord);
            Users _user = await _loginRepository.login(user);
            if (_user != null && user.vchUserName == _user.vchUserName && user.vchPassWord == _user.vchPassWord)
            {
                try
                {
                    HttpContext.Session.SetInt32("_UserId", Convert.ToInt32(_user.intUserId));
                    HttpContext.Session.SetString("_Role", Convert.ToString(_user.vchUserName));
                    HttpContext.Session.SetString("_Name", Convert.ToString(_user.vchFullName));
                }
                catch(Exception ex)
                {
                    throw;
                }
               
                return _user;
            }
            return null;
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Include necessary claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.vchFullName),
                new Claim(ClaimTypes.Sid, user.intUserId.ToString()),
                new Claim(ClaimTypes.Role, user.vchUserName),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Users user)
            {
            IActionResult response = Unauthorized();

            // Authenticate user and fetch the user details
            var authenticatedUser = await Authenticatedusers(user);

            if (authenticatedUser != null)
            {
                var token = GenerateToken(authenticatedUser);
                string message = "Login successful";

                response = Ok(new
                {
                    token = token,
                    message = message,
                    role = authenticatedUser.vchUserName,
                    fullName = authenticatedUser.vchFullName
                });
            }
            else
            {
                response = Unauthorized(new { message = "Invalid username or password" });
            }

            return response;
        }




        //[HttpPost("UserRegistration")]
        //public async Task<IActionResult> UserRegistration([FromBody] Registration registration)
        //{

        //    if (registration == null)
        //    {
        //        return BadRequest("Provide All The Data");
        //    }
        //    if (registration.vchPassWord != null)
        //    {
        //        var password = Md5Encryption.MD5Encryption(registration.vchPassWord);
        //        registration.vchPassWord = password;
        //    }
        //    try
        //    {
        //        var regd = await _loginRepository.Registration(registration);
        //        return Ok(regd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}
        [HttpPost("UserRegistration")]
        public async Task<IActionResult> UserRegistration([FromBody] dynamic requestedAllData)
        {
            try
            {
                if (requestedAllData == null || requestedAllData.REQUEST_DATA == null)
                {
                    return BadRequest("Invalid REQUEST_DATA.");
                }
                string base64DecodedData = Encoding.UTF8.GetString(Convert.FromBase64String((string)requestedAllData.REQUEST_DATA));
                dynamic requestedData = JsonConvert.DeserializeObject(base64DecodedData);
                var req = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(requestedData));
                var registration = new Registration
                {
                    vchUserName = req.ContainsKey("vchUserName") ? req["vchUserName"]?.ToString() : null,
                    vchPassWord = req.ContainsKey("vchPassWord") ? req["vchPassWord"]?.ToString() : null,
                    vchFullName = req.ContainsKey("vchFullName") ? req["vchFullName"]?.ToString() : null,
                    intLevelDetailId = req.ContainsKey("intLevelDetailId") ? Convert.ToInt32(req["intLevelDetailId"]) : 0,
                    intDesignationId = req.ContainsKey("intDesignationId") ? Convert.ToInt32(req["intDesignationId"]) : 0,
                    vchMobTel = req.ContainsKey("vchMobTel") ? req["vchMobTel"]?.ToString() : null,
                    vchEmail = req.ContainsKey("vchEmail") ? req["vchEmail"]?.ToString() : null,
                    vchGender = req.ContainsKey("vchGender") ? req["vchGender"]?.ToString() : null,
                    intGroupID = req.ContainsKey("intGroupID") ? Convert.ToInt32(req["intGroupID"]) : 0,
                    vchOffTel = req.ContainsKey("vchOffTel") ? req["vchOffTel"]?.ToString() : null,
                    bitStatus = req.ContainsKey("bitStatus") ? Convert.ToInt32(req["bitStatus"]) : 0,
                    intCreatedBy = req.ContainsKey("intCreatedBy") ? req["intCreatedBy"]?.ToString() : null
                };
                if (registration == null || string.IsNullOrWhiteSpace(registration.vchPassWord))
                {
                    return BadRequest("Provide all the required data.");
                }
                registration.vchPassWord = Md5Encryption.MD5Encryption(registration.vchPassWord);
                var regd = await _loginRepository.Registration(registration);
                if (regd != null)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Registration successful",
                        Data = regd
                    });
                }

                return BadRequest(new
                {
                    Success = false,
                    Message = "Registration failed. Please try again."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An internal server error occurred.",
                    Error = ex.Message
                });
            }
        }
    }
}

