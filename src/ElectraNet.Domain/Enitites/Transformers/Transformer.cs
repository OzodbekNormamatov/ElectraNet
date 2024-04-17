using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Transformers;

public class Transformer : Auditable
{
    public string Description { get; set; }
    public long TransformerPointId { get; set; }
}
