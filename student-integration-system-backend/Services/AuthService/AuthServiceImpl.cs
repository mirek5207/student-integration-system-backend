using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Services.UserRoleService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.AuthService;

public class AuthServiceImpl : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IUserRoleService _userRoleService;
    private readonly IUserService _userService;

    public AuthServiceImpl(AppDbContext dbContext, IConfiguration configuration, IUserRoleService userRoleService, IUserService userService)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _userRoleService = userRoleService;
        _userService = userService;
    }
    public AuthenticationResponse AuthUser(SignInRequest request)
    {
        var user = _userService.GetUserByLogin(request.Login);
        return GenerateJwtToken(user);
    }

    public AuthenticationResponse GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
        var claims = CreateClaim(user);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddHours(24),
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
        var role = _userRoleService.GetUserRole(user);
        var claims = new List<Claim>();
        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, role.Name));
        return claims;
    }
}