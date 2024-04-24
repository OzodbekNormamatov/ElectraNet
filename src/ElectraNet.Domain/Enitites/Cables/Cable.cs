using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Commons;

namespace ElectraNet.Domain.Enitites.Cables;
public class Cable : Auditable
{
    /// <summary>
    /// Represents the voltage associated with an asset.
    /// </summary>
    public long Voltage { get; set; }

    /// <summary>
    /// Represents a description associated with an asset.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Represents the identifier of the asset associated with the entity.
    /// </summary>
    public long AssetId { get; set; }

    /// <summary>
    /// Represents the asset associated with the entity.
    /// </summary>
    public Asset Asset { get; set; }
}
