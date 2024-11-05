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

        private async Task<Users> Authenticatedusers(Users user)
        {
            user.vchPassWord = Md5Encryption.MD5Encryption(user.vchPassWord);
            Users _user = await _loginRepository.login(user);
            if (_user != null && user.vchUserName == _user.vchUserName && user.vchPassWord == _user.vchPassWord)
            {
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
    }
}

