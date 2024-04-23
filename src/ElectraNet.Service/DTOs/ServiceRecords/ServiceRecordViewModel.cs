using ElectraNet.Domain.Enums;
using ElectraNet.Service.DTOs.Employees;

namespace ElectraNet.Service.DTOs.ServiceRecords;

public class ServiceRecordViewModel
{
    public long Id { get; set; }
    public long? CableId { get; set; }
    public long? TransformerPointId { get; set; }
    public EmployeeViewModel Employee { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ServiceStatus Status { get; set; }
}