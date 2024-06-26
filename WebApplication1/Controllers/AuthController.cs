using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Auth")]
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
        public IActionResult Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (_context.Users.Any(u => u.Username == userForRegisterDto.Username))
            {
                return Conflict("Username already exists");
            }

            if(_context.Users.Any(u => u.Email == userForRegisterDto.Email))
            {
                return Conflict("Email already associated to another user");
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
            if (string.IsNullOrEmpty(user.Salt))
            {
                return BadRequest("Salt is missing for the user");
            }
            if (string.IsNullOrEmpty(userForLoginDto.Password))
            {
                return BadRequest("Password cannot be null or empty");
            }

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
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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