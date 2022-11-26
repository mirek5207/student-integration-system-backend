using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
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
    private readonly IAccountService _accountService;
    private readonly IUserRoleService _userRoleService;

    public ClientServiceImpl(IUserService userService, AppDbContext dbContext, IAuthService authService,
        IRoleService roleService, IAccountService accountService, IUserRoleService userRoleService)
    {
        _userService = userService;
        _dbContext = dbContext;
        _authService = authService;
        _roleService = roleService;
        _accountService = accountService;
        _userRoleService = userRoleService;
    }

    public AuthenticationResponse RegisterClient(ClientSignUpRequest request)
    {
        var role = _roleService.GetRoleById(RoleType.ClientId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword);
        _userRoleService.CreateUserRole(user, role);
        _accountService.CreateAccount(user);
        CreateClient(user, request.FirstName, request.SurName);
        return _authService.GenerateJwtToken(user);
    }

    public Client UpdateClient(UpdateClientRequest request, int userId)
    {
        var client = GetClientById(userId);

        client.FirstName = request.FirstName;
        client.SurName = request.SurName;
        client.User.Email = _userService.CheckIfEmailIsUnique(client.User.Id,request.Email) ? request.Email : throw new BadRequestException("Email already exist");
        client.User.Login = _userService.CheckIfLoginIsUnique(client.User.Id,request.Login) ? request.Login : throw new BadRequestException("Login already exist");
        client.User.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.HashedPassword);
        _accountService.UpdateStatusOfUserAccount(userId, request.IsActive);
        _dbContext.SaveChanges();
        
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
    
    public Client GetClientById(int userId)
    {
        var client = _dbContext.Clients.Include(client => client.User.Account).FirstOrDefault(client => client.User.Id == userId);
        if (client == null) throw new NotFoundException("Client not found");
        return client;
    }

    public IEnumerable<Client> GetAllClients()
    {
        var clients = _dbContext.Clients.Include(client => client.User.Account).ToList();
        if (clients == null) throw new NotFoundException("Clients not found");
        return clients;
    }

    public Client GetClientByUserId(int userId)
    {
        var client = _dbContext.Clients.FirstOrDefault(client => client.UserId == userId);
        if (client == null) throw new NotFoundException("Client not found");
        return client;
    }
}