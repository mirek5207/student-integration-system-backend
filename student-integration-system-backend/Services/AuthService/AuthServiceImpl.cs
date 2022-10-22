using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Services.UserRoleService;

namespace student_integration_system_backend.Services.AuthService;

public class AuthServiceImpl : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IUserRoleService _userRoleService;

    public AuthServiceImpl(AppDbContext dbContext, IConfiguration configuration, IUserRoleService userRoleService)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _userRoleService = userRoleService;
    }
    
    public AuthenticationResponse GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
        var claims = CreateClaim(user);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var authenticationResponse = new AuthenticationResponse
        {
            Token = tokenHandler.WriteToken(token)
        };
        return authenticationResponse;
    }

    private List<Claim> CreateClaim(User user)
    {
        var role = _userRoleService.GetRole(user);
        var claims = new List<Claim>();
        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, role.Name));
        return claims;
    }
}