using ElectraNet.Service.DTOs.Organizations;
using ElectraNet.Service.DTOs.Positions;
using ElectraNet.Service.DTOs.Users;

namespace ElectraNet.Service.DTOs.Employees;

public class EmployeeViewModel
{
    public long Id { get; set; }
    public OrganizationViewModel Organization { get; set; }
    public UserViewModel User { get; set; }
    public PositionViewModel Position { get; set; }
}
