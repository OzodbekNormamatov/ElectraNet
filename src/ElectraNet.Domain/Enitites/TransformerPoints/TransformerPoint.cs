using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Organizations;

namespace ElectraNet.Domain.Enitites.TransformerPoints;

public class TransformerPoint : Auditable
{
    public string Title { get; set; }
    public string Address { get; set; }
    public string OrganizationId { get; set; }
    public Organization Organization { get; set; }
}
