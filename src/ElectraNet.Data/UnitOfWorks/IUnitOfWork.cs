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

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Cable> Cables { get; }
    IRepository<UserRole> UserRoles { get; }
    IRepository<Laboratory> Laboratories { get; }
    IRepository<TransformerPoint> TransformerPoints { get; }
    IRepository<Position> Positions { get; }
    IRepository<ServiceRecord> ServiceRecord { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<UserPermission> UserPermissions { get; }
    IRepository<Employee> Employees { get; }
    IRepository<Organization> Organizations { get; }
    IRepository<Transformer> Transformers { get; }
    ValueTask<bool> SaveAsync();
}