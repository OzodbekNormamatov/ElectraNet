using ElectraNet.Domain.Enums;

namespace ElectraNet.Service.DTOs.Laboratories;

public class LaboratoryUpdateModel
{
    public long? CableId { get; set; }
    public long? TransformerPointId { get; set; }
    public long MasterId { get; set; }
    public long FirstVoltage { get; set; }
    public long SecondVoltage { get; set; }
    public long ThirdVoltage { get; set; }
    public LaboratoryStatus Status { get; set; }
}
