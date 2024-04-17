using ElectraNet.Domain.Enums;
using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Laboratories;

public class Laboratory:Auditable
{
    public long? CableId { get; set; }
    public Cable Cable { get; set; }
    public long? TransformerPointId { get; set; }
    public TransformerPoint TransformerPoint { get; set; }
    public long MasterId { get; set; }
    public Employee Employee { get; set; }
    public long FirstVoltage { get; set; }
    public long SecondVoltage { get; set; }
    public long ThirdVoltage { get; set; }
    public LaboratoryStatus Status {  get; set; }
}