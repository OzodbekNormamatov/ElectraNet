using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.TransformerPoints;

namespace ElectraNet.Domain.Enitites.Transformers;

public class Transformer : Auditable
{
    public string Description { get; set; }
    public long TransformerPointId { get; set; }
    public TransformerPoint TransformerPoint { get; set; }
}
