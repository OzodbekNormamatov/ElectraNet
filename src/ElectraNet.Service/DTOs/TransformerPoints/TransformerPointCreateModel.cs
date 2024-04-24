using ElectraNet.Domain.Enitites.Organizations;

namespace ElectraNet.Service.DTOs.TransformerPoints;

public class TransformerPointCreateModel
{
    public string Title { get; set; }
    public string Address { get; set; }
    public long OrganizationId { get; set; }
}
