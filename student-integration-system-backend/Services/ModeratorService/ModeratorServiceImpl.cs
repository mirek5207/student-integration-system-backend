using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.AccountService;
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
    private readonly IAccountService _accountService;

    public ModeratorServiceImpl(AppDbContext dbContext, IUserService userService, IAuthService authService, IRoleService roleService, IAccountService accountService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _authService = authService;
        _roleService = roleService;
        _accountService = accountService;
    }

    public AuthenticationResponse RegisterModerator(ModeratorSignUpRequest request)
    {
        var role = _roleService.GetRoleById(RoleType.ModeratorId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        CreateModerator(user,request.FirstName, request.SurName);
        
        return _authService.GenerateJwtToken(user);
    }

    public Moderator UpdateModerator(UpdateModeratorRequest request, int moderatorId)
    {
        var moderator = GetModeratorById(moderatorId);
        var user = _userService.GetUserById(moderator.UserId);
        var account = _accountService.GetAccountByUserId(moderator.UserId);
        
        moderator.FirstName = request.FirstName;
        moderator.SurName = request.SurName;
        user.Email = _userService.CheckIfEmailIsUnique(user.Id,request.Email) ? request.Email : throw new BadRequestException("Email already exist");
        user.Login = _userService.CheckIfLoginIsUnique(user.Id,request.Login) ? request.Login : throw new BadRequestException("Login already exist");
        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.HashedPassword);
        account.IsActive = request.IsAccountActive;
        _dbContext.SaveChanges();
        
        return moderator;
    }

    public IEnumerable<Moderator> GetAllModerators()
    {
        var moderators = _dbContext.Moderators.Include(moderator => moderator.User.Account).ToList();
        if (moderators == null) throw new NotFoundException("Not found any moderator");
        return moderators;
    }

    private Moderator GetModeratorById(int moderatorId)
    {
        var moderator = _dbContext.Moderators.FirstOrDefault(moderator => moderator.Id == moderatorId);
        if (moderator == null) throw new NotFoundException("Moderator not found");
        return moderator;
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