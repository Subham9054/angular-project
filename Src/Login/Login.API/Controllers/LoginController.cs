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

        private async Task<string> Authenticatedusers(Users user)
        {
            user.vchPassWord = Md5Encryption.MD5Encryption(user.vchPassWord);
            Users _user = await _loginRepository.login(user);
            if (_user != null)
            {
                if (user.vchUserName == _user.vchUserName && user.vchPassWord == _user.vchPassWord)
                {
                    return "User found and authenticated";
                }
                else
                {
                    return "Invalid username or password";
                }
            }
            else
            {
                return "User not found";
            }
        }
        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.vchUserName),
                new Claim(ClaimTypes.Role, user.Role),  
               // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),  // Set a suitable token expiration time
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            if (!string.IsNullOrEmpty(user.vchUserName) && !string.IsNullOrEmpty(user.vchPassWord))
            {
                ActionResult response = Unauthorized();
                string authMessage = await Authenticatedusers(user);
                if (authMessage == "User found and authenticated")
                {
                    var token = GenerateToken(user);
                    string message = "Login successful! Welcome " + user.vchUserName;
                    response = Ok(new { token = token, message = message, role = user.Role });
                }
                else
                {
                    response = Unauthorized(new { message = authMessage });
                }
                return response;
            }

            else
            {
                // Handle missing username or password
                return BadRequest(new { message = "Username or password cannot be null or empty." });
            }
            //public async Task<IActionResult> Login([FromBody] Users user)
            //{
            //    IActionResult response = Unauthorized();
            //    string authMessage = await Authenticatedusers(user);
            //    if (authMessage == "User found and authenticated")
            //    {
            //        var token = GenerateToken(user);
            //        string message = "Login successful! Welcome " + user.vchUserName;
            //        response = Ok(new { token = token, message = message });
            //    }
            //    else
            //    {
            //        response = Unauthorized(new { message = authMessage });
            //    }
            //    return response;
            //}


        }
    }
}

