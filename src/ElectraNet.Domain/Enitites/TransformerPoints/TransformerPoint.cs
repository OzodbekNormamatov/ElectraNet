using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Organizations;

namespace ElectraNet.Domain.Enitites.TransformerPoints;

public class TransformerPoint : Auditable
{
    /// <summary>
    /// Represents the title or name associated with an entity.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Represents the address associated with an entity.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Represents the identifier of the organization associated with an entity.
    /// </summary>

    public long OrganizationId { get; set; }

    /// <summary>
    /// Represents the organization associated with an entity.
    /// </summary>
    public Organization Organization { get; set; }
}