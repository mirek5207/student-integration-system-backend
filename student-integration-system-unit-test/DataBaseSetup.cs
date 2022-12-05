using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;

namespace student_integration_system_unit_test;

public static class DataBaseSetup
{

    public static AppDbContext SetUpDataBase()
    {
        var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("AppDb")
            .Options;
        return new AppDbContext(dbContextOptions);
    }
}