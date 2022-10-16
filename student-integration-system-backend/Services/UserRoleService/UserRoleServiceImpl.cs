using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.UserRoleService;

public class UserRoleServiceImpl : IUserRoleService
{
    private readonly AppDbContext _dbContext;

    public UserRoleServiceImpl(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateUserRole(User user, Role role)
    {
        var userRole = new UserRole
        {
            User = user,
            Role = role
        };
        _dbContext.UserRoles.Add(userRole);
        _dbContext.SaveChangesAsync();
    }
}