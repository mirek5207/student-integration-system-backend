using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.AuthService;
using student_integration_system_backend.Services.RoleService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.ModeratorService;

public class ModeratorServiceImpl : IModeratorService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IRoleService _roleService;

    public ModeratorServiceImpl(AppDbContext dbContext, IUserService userService, IAuthService authService, IRoleService roleService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _authService = authService;
        _roleService = roleService;
    }

    public AuthenticationResponse RegisterModerator(ModeratorSignUpRequest request)
    {
        var role = _roleService.GetRoleById(RoleType.ModeratorId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        CreateModerator(user,request.FirstName, request.SurName);
        
        return _authService.GenerateJwtToken(user);
    }

    private Moderator CreateModerator(User user, string firstName, string surname)
    {
        var moderator = new Moderator
        {
            User = user, 
            FirstName = firstName,
            SurName = surname
        };
        _dbContext.Moderators.Add(moderator);
        _dbContext.SaveChanges();
        return moderator;
    }
    
}