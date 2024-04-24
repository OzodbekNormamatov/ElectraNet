using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Domain.Enitites.Employees;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Domain.Enums;

namespace ElectraNet.Domain.Enitites.ServiceRecords;

public class ServiceRecord:Auditable
{
    /// <summary>
    /// Represents the identifier of a cable associated with the service, if applicable.
    /// </summary>
    public long? CableId { get; set; }

    /// <summary>
    /// Represents the cable associated with the service.
    /// </summary>
    public Cable Cable { get; set; }

    /// <summary>
    /// Represents the identifier of a transformer point associated with the service, if applicable.
    /// </summary>
    public long? TransformerPointId { get; set; }

    /// <summary>
    /// Represents the transformer point associated with the service.
    /// </summary>
    public TransformerPoint TransformerPoint { get; set; }

    /// <summary>
    /// Represents the identifier of the master employee responsible for the service.
    /// </summary>
    public long MasterId { get; set; }

    /// <summary>
    /// Represents the master employee responsible for the service.
    /// </summary>
    public Employee Employee { get; set; }

    /// <summary>
    /// Represents a description of the service.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Represents the start time of the service.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Represents the end time of the service.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Represents the status of the service.
    /// </summary>
    public ServiceStatus Status { get; set; }
}