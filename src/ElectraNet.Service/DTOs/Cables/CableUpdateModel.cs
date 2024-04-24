using ElectraNet.Domain.Enitites.Commons;

namespace ElectraNet.Service.DTOs.Cables;

public class CableUpdateModel
{
    public long Voltage { get; set; }
    public string Description { get; set; }
    public long AssetId { get; set; }
}
