using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Seeds;

namespace student_integration_system_backend.Data.Import;

public class RolesImport : IDataImport
{
    public void Seed(ModelBuilder builder)
    {
        var roles = new List<Role>
        {
            new()
            {
                Id = RoleType.AdminId,
                Name = RoleType.Admin
            },
            new()
            {
                Id = RoleType.ClientId,
                Name = RoleType.Client
            },
            new()
            {
                Id = RoleType.ModeratorId,
                Name = RoleType.Moderator
            },
            new()
            {
                Id = RoleType.PlaceOwnerId,
                Name = RoleType.PlaceOwner
            },
        };
        builder.Entity<Role>().HasData(roles);
    }
}