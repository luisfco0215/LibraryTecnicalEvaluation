using LibraryTecnicalEvaluation.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryTecnicalEvaluation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AutorizacionController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AutorizacionController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (dto.Username == "admin" && dto.Password == "admin123")
            {
                var token = GenerateToken(dto.Username, "Admin");
                return Ok(new { token });
            }

            if (dto.Username == "user" && dto.Password == "user123")
            {
                var token = GenerateToken(dto.Username, "User");
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateToken(string username, string role)
        {
            var jwtKey = _config["Jwt:Key"]!;
            var jwtIssuer = _config["Jwt:Issuer"]!;
            var jwtAudience = _config["Jwt:Audience"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
