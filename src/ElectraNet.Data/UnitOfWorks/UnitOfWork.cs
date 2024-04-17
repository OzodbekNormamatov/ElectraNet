using ElectraNet.DataAccess.Repositories;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Domain.Enitites.Laboratories;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.ServiceRecords;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Domain.Enitites.Users;
using System.Security;

namespace ElectraNet.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;


    public IRepository<User> Users { get; }
    public IRepository<Asset> Assets { get; }
    public IRepository<Cable> Cables { get; }
    public IRepository<UserRole> UserRoles { get; }
    public IRepository<Laboratory> Laboratories { get; }
    public IRepository<TransformerPoint> TransformerPoints { get; }
    public IRepository<Position> Positions { get; }
    public IRepository<ServiceRecord> ServiceRecord { get; }
    public IRepository<Permission> Permissions { get; }
    public IRepository<UserPermission> UserPermissions { get; }
    public IRepository<Employee> Employees { get; }
    public IRepository<Organization> Organizations { get; }
    public IRepository<Transformer> Transformers { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new Repository<User>(_context);
        Assets = new Repository<Asset>(_context);
        Students = new Repository<Student>(_context);
        UserRoles = new Repository<UserRole>(_context);
        Instructors = new Repository<Instructor>(_context);
        Permissions = new Repository<Permission>(_context);
        UserPermissions = new Repository<UserPermission>(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
