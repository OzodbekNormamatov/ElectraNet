using ElectraNet.Domain.Commons;
namespace ElectraNet.Domain.Enitites.Organizations;
public class Organization : Auditable
{
    /// <summary>
    /// Represents a name associated with an entity.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Represents an address associated with an entity.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Represents a phone number associated with an entity.
    /// </summary>
    public string PhoneNumber { get; set; }
}
