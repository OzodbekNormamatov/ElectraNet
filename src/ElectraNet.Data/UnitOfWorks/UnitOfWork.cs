using ElectraNet.DataAccess.Repositories;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Domain.Enitites.Employees;
using ElectraNet.Domain.Enitites.Laboratories;
using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.ServiceRecords;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Domain.Enitites.Users;

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
    public IRepository<ServiceRecord> ServiceRecords { get; }
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
        Cables = new Repository<Cable>(_context);
        Employees = new Repository<Employee>(_context);
        Positions = new Repository<Position>(_context);
        UserRoles = new Repository<UserRole>(_context);
        Permissions = new Repository<Permission>(_context);
        Laboratories = new Repository<Laboratory>(_context);
        Transformers = new Repository<Transformer>(_context);
        Organizations = new Repository<Organization>(_context);
        ServiceRecords = new Repository<ServiceRecord>(_context);
        UserPermissions = new Repository<UserPermission>(_context);
        TransformerPoints =  new Repository<TransformerPoint>(_context);    
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
