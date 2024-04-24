using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Domain.Enitites.Employees;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Domain.Enums;

namespace ElectraNet.Domain.Enitites.Laboratories;

public class Laboratory : Auditable
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
    /// Represents the voltage of the first phase.
    /// </summary>
    public long FirstVoltage { get; set; }

    /// <summary>
    /// Represents the voltage of the second phase.
    /// </summary>
    public long SecondVoltage { get; set; }

    /// <summary>
    /// Represents the voltage of the third phase.
    /// </summary>
    public long ThirdVoltage { get; set; }

    /// <summary>
    /// Represents the status of the service in the laboratory.
    /// </summary>
    public LaboratoryStatus Status { get; set; }
}