using ElectraNet.Service.DTOs.Organizations;

namespace ElectraNet.Service.DTOs.TransformerPoints;

public class TransformerPointViewModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
    public OrganizationViewModel Organization { get; set; }
}
