using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.UserRoleService;

public interface IUserRoleService
{
    void CreateUserRole(User user, Role role);
    Role GetUserRole(User user);
}