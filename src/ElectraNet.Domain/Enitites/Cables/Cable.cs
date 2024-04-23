using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enitites.Commons;

namespace ElectraNet.Domain.Enitites.Cables;
public class Cable : Auditable
{
    public long Voltage {  get; set; }
    public string Description {  get; set; }
    public long AssetId {  get; set; }
    public Asset Asset { get; set; }
}