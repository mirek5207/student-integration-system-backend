using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Data.Import;

namespace student_integration_system_backend.Entities;

public class AppDbContext : DbContext
{
    private readonly IEnumerable<IDataImport> _dataImport;
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext>options, IEnumerable<IDataImport> dataImport) : base(options)
    {
        _dataImport = dataImport;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var dataImport in _dataImport)
        {
            dataImport.Seed(modelBuilder);
        }
    }

}