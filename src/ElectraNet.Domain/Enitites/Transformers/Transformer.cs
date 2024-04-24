using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.TransformerPoints;

namespace ElectraNet.Domain.Enitites.Transformers;

public class Transformer : Auditable
{
    /// <summary>
    /// Represents a description associated with an entity.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Represents the identifier of a transformer point.
    /// </summary>
    public long? TransformerPointId { get; set; }

    /// <summary>
    /// Represents the transformer point associated with an entity.
    /// </summary>
    public TransformerPoint TransformerPoint { get; set; }
}
