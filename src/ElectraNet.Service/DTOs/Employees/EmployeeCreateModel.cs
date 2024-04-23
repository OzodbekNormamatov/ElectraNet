using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.Users;

namespace ElectraNet.Service.DTOs.Employees;

public class EmployeeCreateModel
{
    public long OrganizationId { get; set; }
    public long UserId { get; set; }
    public long PositionId { get; set; }
}
