using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Domain.Enums;

namespace ElectraNet.Domain.Enitites.ServiceRecords;

public class ServiceRecord:Auditable
{
    public long? CableId { get; set; }
    public Cable Cable { get; set; }
    public long? TransformerPointId { get; set; }
    public TransformerPoint TransformerPoint { get; set; }
    public long MasterId { get; set; }
    public Employee Employee { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ServiceStatus Status { get; set; }
}
