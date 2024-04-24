using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Positions;

public class Position : Auditable
{
    /// <summary>
    /// Represents a name associated with an entity.
    /// </summary>
    public string Name { get; set; }
}
