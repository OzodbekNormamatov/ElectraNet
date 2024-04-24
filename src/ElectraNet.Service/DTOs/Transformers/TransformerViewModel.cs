using ElectraNet.Service.DTOs.TransformerPoints;

namespace ElectraNet.Service.DTOs.Transformers;

public class TransformerViewModel
{
    public long Id { get; set; }
    public string Description { get; set; }
    public TransformerPointViewModel TransformerPoint { get; set; }
}
