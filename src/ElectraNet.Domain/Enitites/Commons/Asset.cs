using ElectraNet.Domain.Enums;
using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Commons;

public class Asset:Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public FileType FileType { get; set; }
}
