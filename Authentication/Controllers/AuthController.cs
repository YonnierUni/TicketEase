using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Authentication.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;


namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        public AuthController(IOptions<JwtSettings> jwtSettings)
        {
            _secretKey = jwtSettings.Value.Key;
            _issuer = jwtSettings.Value.Issuer;
            _audience = jwtSettings.Value.Audience;
        }

        private readonly List<User> _users = new List<User>
        {
            new User { UserName = "admin", Password = "password", Role = "admin" },
            new User { UserName = "client", Password = "password", Role = "client" }
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.LoginRequest loginRequest)
        {
            var user = _users.SingleOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token, Role = user.Role });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role) // Agregar el rol al claim
            };

            // Crear la clave de firma (clave secreta)
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            // Crear las credenciales de firma
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear el token JWT con los parámetros necesarios
            var token = new JwtSecurityToken(_issuer, _audience, claims,
                                              expires: DateTime.Now.AddHours(1), // Configura el tiempo de expiración
                                              signingCredentials: credentials);

            // Retornar el token como cadena de texto
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
