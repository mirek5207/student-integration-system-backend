using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Data.Import;

namespace student_integration_system_backend.Entities;

public class AppDbContext : DbContext
{
    private readonly IEnumerable<IDataImport> _dataImport;
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<CustomPlace> CustomPlaces { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Lobby> Lobbies { get; set; }
    public DbSet<LobbyGuest> LobbyGuests { get; set; }
    public DbSet<LobbyOwner> LobbyOwners { get; set; }
    public DbSet<Moderator> Moderators { get; set; }
    public DbSet<PlaceOwner> PlacesOwners { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext>options, IEnumerable<IDataImport> dataImport) : base(options)
    {
        _dataImport = dataImport;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasKey(sc => new { sc.UserId, sc.RoleId });
        modelBuilder.Entity<LobbyGuest>().HasKey(r => new { r.LobbyId, r.ClientId });
        foreach (var dataImport in _dataImport)
        {
            dataImport.Seed(modelBuilder);
        }
    }

}