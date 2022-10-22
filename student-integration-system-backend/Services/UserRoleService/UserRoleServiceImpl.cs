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
        _dbContext.SaveChanges();
    }

    public Role GetRole(User user)
    {
        var userRole = _dbContext.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
        var role = _dbContext.Roles.FirstOrDefault(r => userRole != null && r.Id == userRole.RoleId);
        return role;
    }
}