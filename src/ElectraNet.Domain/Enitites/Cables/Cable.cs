using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Cables;
public class Cable:Auditable
{
    public long Voltage {  get; set; }
    public string Description {  get; set; }
    public long Assetid {  get; set; }
    public AssetId AssetId { get; set; }
}
