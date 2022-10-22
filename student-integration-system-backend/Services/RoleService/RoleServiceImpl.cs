using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;

namespace student_integration_system_backend.Services.RoleService;

public class RoleServiceImpl : IRoleService
{
    private readonly AppDbContext _dbContext;

    public RoleServiceImpl(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Role GetRoleById(int roleId)
    {
        var role = _dbContext.Roles.Find(roleId);
        if (role == null) throw new NotFoundException("Role not found");
        return role;
    }
}