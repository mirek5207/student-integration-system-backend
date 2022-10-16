using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Services.ModeratorService;

public class ModeratorServiceImpl : IModeratorService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;

    public ModeratorServiceImpl(AppDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public Moderator RegisterModerator(ModeratorSignUpRequest request)
    {
        var role = _dbContext.Roles.Find(RoleType.ModeratorId);
        var user = _userService.CreateUser(request.Login, request.Email, request.HashedPassword, role);
        var moderator = CreateModerator(user,request.FirstName, request.SurName);
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