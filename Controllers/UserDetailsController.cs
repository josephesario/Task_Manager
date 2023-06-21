using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Task_Manager.Models;
using Task_Manager.Secure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Task_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly taskManagerDbContext _context;
        private readonly IConfiguration _configuration;

        public UserDetailsController(taskManagerDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] TUserDetail userDetail)
        {
            try
            {
                // Check if the email is already in use
                bool emailExists = _context.TUserDetails.Any(u => u.Email == userDetail.Email);
                if (emailExists)
                {
                    return Conflict("User already available with this email");
                }

                // Hash the password
                userDetail.Password = PasswordHasher.HashPassword(userDetail.Password);
                userDetail.CreatedOn = DateTime.UtcNow;

                // Save the user details to the database
                _context.TUserDetails.Add(userDetail);
                _context.SaveChanges();

                return Ok($"Hello {userDetail.Email}, you have registered successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] TUserDetail loginModel)
        {
            try
            {
                // Retrieve the user details from the database based on the provided email
                var userDetails = _context.TUserDetails.FirstOrDefault(u => u.Email == loginModel.Email);

                // Check if the user exists and if the provided password matches the hashed password
                if (userDetails != null && PasswordHasher.VerifyPassword(loginModel.Password, userDetails.Password))
                {
                    // Authentication successful, generate a token
                    var token = GenerateJwtToken(userDetails.Email, "Admin");
                    return Ok(new { Token = token });
                }
                else
                {
                    // Authentication failed
                    return Unauthorized("Authentication failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        private string GenerateJwtToken(string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
            }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:TokenExpirationHours"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpGet("Get_UserDetails/{email}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserDetails(string email)
        {
            var userDetails = _context.TUserDetails.FirstOrDefault(u => u.Email == email);

            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            else
            {
                return NotFound();
            }
        }

     
    }
}
