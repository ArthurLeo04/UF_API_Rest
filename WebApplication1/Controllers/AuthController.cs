using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;

        public AuthController(DatabaseContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            if (_context.Users.Any(u => u.Username == userForRegisterDto.Username))
            {
                return Conflict("Username already exists");
            }

            byte[] salt = GenerateSalt();
            byte[] passwordHash = HashPassword(userForRegisterDto.Password, salt);

            var user = new Users
            {
                Id = Guid.NewGuid(),
                Email = userForRegisterDto.Email,
                Username = userForRegisterDto.Username,
                Password = Convert.ToBase64String(passwordHash),
                Salt = Convert.ToBase64String(salt),
                Rank = "Bronze",
                Role = "client"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == userForLoginDto.Username);
            if (user == null)
                return Unauthorized();

            byte[] salt = Convert.FromBase64String(user.Salt);
            byte[] hashedPassword = HashPassword(userForLoginDto.Password, salt);
            string hashedPasswordString = Convert.ToBase64String(hashedPassword);

            if (user.Password != hashedPasswordString)
                return Unauthorized();

            // Authentication successful, generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    // Add additional claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = passwordBytes.Concat(salt).ToArray();
                return sha256.ComputeHash(saltedPassword);
            }
        }
    }
}