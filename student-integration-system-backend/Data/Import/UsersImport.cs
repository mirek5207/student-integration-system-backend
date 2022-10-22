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
                Login = RoleType.Admin,
                Email = RoleType.Admin,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(RoleType.Admin),
            },
            new()
            {
                Id = RoleType.ModeratorId, 
                Email = RoleType.Moderator,
                Login = RoleType.Moderator,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(RoleType.Moderator),
            }
        };
        builder.Entity<User>().HasData(users);
    }
}