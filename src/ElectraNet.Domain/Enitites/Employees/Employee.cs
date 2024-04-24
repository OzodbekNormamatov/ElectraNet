using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.Users;

namespace ElectraNet.Domain.Enitites.Employees;

public class Employee : Auditable
{
    /// <summary>
    /// Represents the identifier of an organization associated with the entity, if applicable.
    /// </summary>
    public long? OrganizationId { get; set; }

    /// <summary>
    /// Represents the organization associated with the entity.
    /// </summary>
    public Organization Organization { get; set; }

    /// <summary>
    /// Represents the identifier of a user associated with the entity, if applicable.
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// Represents the user associated with the entity.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Represents the identifier of a position associated with the entity, if applicable.
    /// </summary>
    public long? PositionId { get; set; }

    /// <summary>
    /// Represents the position associated with the entity.
    /// </summary>
    public Position Position { get; set; }
}
