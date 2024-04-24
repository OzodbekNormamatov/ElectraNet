using Microsoft.EntityFrameworkCore;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Domain.Enitites.Employees;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Domain.Enitites.Laboratories;
using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.ServiceRecords;
using ElectraNet.Domain.Enitites.TransformerPoints;

namespace ElectraNet;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cable>()
            .HasOne(cable => cable.Asset)
            .WithMany()
            .HasForeignKey(cable => cable.AssetId);

        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.User)
            .WithMany()
            .HasForeignKey(employee => employee.UserId);

        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Organization)
            .WithMany()
            .HasForeignKey(employee => employee.OrganizationId);

        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Position)
            .WithMany()
            .HasForeignKey(employee => employee.PositionId);

        modelBuilder.Entity<Laboratory>()
            .HasOne(laboratory => laboratory.Cable)
            .WithMany()
            .HasForeignKey(laboratory => laboratory.CableId);

        modelBuilder.Entity<Laboratory>()
           .HasOne(laboratory => laboratory.TransformerPoint)
           .WithMany()
           .HasForeignKey(laboratory => laboratory.TransformerPointId);

        modelBuilder.Entity<Laboratory>()
           .HasOne(laboratory => laboratory.Employee)
           .WithMany()
           .HasForeignKey(laboratory => laboratory.MasterId);

        modelBuilder.Entity<ServiceRecord>()
            .HasOne(serviceRecord => serviceRecord.Cable)
            .WithMany()
            .HasForeignKey(serviceRecord => serviceRecord.CableId);

        modelBuilder.Entity<ServiceRecord>()
            .HasOne(serviceRecord => serviceRecord.TransformerPoint)
            .WithMany()
            .HasForeignKey(serviceRecord => serviceRecord.TransformerPointId);

        modelBuilder.Entity<ServiceRecord>()
            .HasOne(serviceRecord => serviceRecord.Employee)
            .WithMany()
            .HasForeignKey(serviceRecord => serviceRecord.MasterId);

        modelBuilder.Entity<TransformerPoint>()
            .HasOne(transformerPoint => transformerPoint.Organization)
            .WithMany()
            .HasForeignKey(transformerPoint => transformerPoint.OrganizationId);

        modelBuilder.Entity<Transformer>()
            .HasOne(transformer => transformer.TransformerPoint)
            .WithMany()
            .HasForeignKey(transformer => transformer.TransformerPointId);

        modelBuilder.Entity<User>()
            .HasOne(user => user.UserRole)
            .WithMany()
            .HasForeignKey(user => user.RoledId);

        modelBuilder.Entity<UserPermission>()
            .HasOne(userPermission => userPermission.Permission)
            .WithMany()
            .HasForeignKey(userPermission => userPermission.PermissionId);
    }

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