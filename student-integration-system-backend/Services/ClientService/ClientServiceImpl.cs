using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.AuthService;
using student_integration_system_backend.Services.RoleService;
using student_integration_system_backend.Services.UserRoleService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.ClientService;

public class ClientServiceImpl : IClientService
{
    private readonly IUserService _userService;
    private readonly AppDbContext _dbContext;
    private readonly IAuthService _authService;
    private readonly IRoleService _roleService;

    public ClientServiceImpl(IUserService userService, AppDbContext dbContext, IAuthService authService, IRoleService roleService)
    {
        _userService = userService;
        _dbContext = dbContext;
        _authService = authService;
        _roleService = roleService;
    }

    public AuthenticationResponse RegisterClient(ClientSignUpRequest request)
    {
        var role = _roleService.GetRoleById(RoleType.ClientId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        CreateClient(user, request.FirstName, request.SurName);
        return _authService.GenerateJwtToken(user);
    }

    private Client CreateClient(User user, string firstName, string surname)
    {
        var client = new Client
        {
            User = user,
            FirstName = firstName,
            SurName = surname
        };
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
        return client;
    }
}