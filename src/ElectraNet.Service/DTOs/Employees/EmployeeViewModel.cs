namespace ElectraNet.Service.DTOs.Employees;

public class EmployeeViewModel
{
    public long Id { get; set; }
    public long? OrganizationId { get; set; }
    public long? UserId { get; set; }
    public long? PositionId { get; set; }
}
