

using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Domain.Enitites.Laboratories;
using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.ServiceRecords;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ElectraNet;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Cable> Cables { get; set; }
    public DbSet<Laboratory> Laboratories { get; set; }
    public DbSet<TransformerPoint> TransformerPoints { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<ServiceRecord> ServiceRecords { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Transformer> Transformers { get; set; }
}