using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Users;

public class Permission : Auditable
{
    /// <summary>
    /// Represents the HTTP method associated with a route or action.
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// Represents the controller name associated with a route or action.
    /// </summary>
    public string Controller { get; set; }
}