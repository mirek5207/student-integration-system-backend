using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.RoleService;

public interface IRoleService
{
    Role GetRoleById(int roleId);
}