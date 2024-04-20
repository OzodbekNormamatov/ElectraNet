using ElectraNet.Domain.Enums;

namespace ElectraNet.Service.DTOs.ServiceRecords;

public class ServiceRecordUpdateModel
{
    public long? CableId { get; set; }
    public long? TransformerPointId { get; set; }
    public long MasterId { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ServiceStatus Status { get; set; }
}
