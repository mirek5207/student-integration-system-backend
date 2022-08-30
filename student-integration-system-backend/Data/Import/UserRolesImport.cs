using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Seeds;

namespace student_integration_system_backend.Data.Import;

public class UserRolesImport : IDataImport
{
    public void Seed(ModelBuilder builder)
    {
        var userRole = new List<UserRole>
        {
            new()
            {
                UserId = RoleType.AdminId,
                RoleId = RoleType.AdminId
            },
            new()
            {
                UserId = RoleType.ModeratorId,
                RoleId = RoleType.ModeratorId
            },
        };
        builder.Entity<UserRole>().HasData(userRole);
    }
}