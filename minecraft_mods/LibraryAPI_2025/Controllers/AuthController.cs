using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.Entities;
using DTO.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI_2025.Controllers;


[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
    {
        var user = await _userManager.FindByNameAsync(loginUser.Nickname);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginUser.Password))
        {
            return Unauthorized(new { Message = "User not found" });
        }
        
        
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = GenerateToken(authClaims);
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }

    
    private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenSettings = _configuration.GetSection("tokenSettings");
        var secretKey = Environment.GetEnvironmentVariable("SECRET") 
                        ?? tokenSettings["SecretKey"]
                        ?? throw new InvalidOperationException("JWT secret is not configured");
        
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        return new JwtSecurityToken(
            issuer: tokenSettings["Issuer"],
            audience: tokenSettings["Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
    }
}