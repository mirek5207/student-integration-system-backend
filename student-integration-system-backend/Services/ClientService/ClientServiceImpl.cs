using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.UserRoleService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.ClientService;

public class ClientServiceImpl : IClientService
{
    private readonly IUserService _userService;
    private readonly AppDbContext _dbContext;

    public ClientServiceImpl(IUserService userService, AppDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }

    public Client RegisterClient(ClientSignUpRequest request)
    {
        var role = _dbContext.Roles.Find(RoleType.ClientId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        var client = CreateClient(user, request.FirstName, request.SurName);
        return client;
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