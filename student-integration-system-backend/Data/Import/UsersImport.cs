using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Seeds;

namespace student_integration_system_backend.Data.Import;

public class UsersImport : IDataImport
{
    public void Seed(ModelBuilder builder)
    {
        var users = new List<User>
        {
            new()
            {
                Id = RoleType.AdminId,
                Email = RoleType.Admin,
                HashedPassword = RoleType.Admin,
                RoleId = RoleType.AdminId
            },
            new()
            {
                Id = RoleType.ModeratorId,
                Email = RoleType.Moderator,
                HashedPassword = RoleType.Moderator,
                RoleId = RoleType.ModeratorId
            }
        };
    }
}