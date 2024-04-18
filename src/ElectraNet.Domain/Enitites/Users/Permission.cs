using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Users;

public class Permission : Auditable
{
    public string Method { get; set; }
    public string Controller { get; set; }
}