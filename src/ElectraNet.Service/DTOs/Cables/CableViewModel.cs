using ElectraNet.Service.DTOs.Assets;

namespace ElectraNet.Service.DTOs.Cables;

public class CableViewModel
{
    public long Id { get; set; }
    public long Voltage { get; set; }
    public string Description { get; set; }
    public AssetViewModel Asset { get; set; }
}