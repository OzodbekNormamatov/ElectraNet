using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.Users;

namespace ElectraNet.Domain.Enitites.Employees;

public class Employee : Auditable
{
    public long? OrganizationId {  get; set; }
    public Organization Organization { get; set; }
    public long? UserId { get; set; }
    public User User { get; set; }
    public long? PositionId { get; set; }
    public Position Position { get; set; }
}
