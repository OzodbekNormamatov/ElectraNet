using ElectraNet.Domain.Commons;
namespace ElectraNet.Domain.Enitites.Organizations;
public class Organization : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
}
