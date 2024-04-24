using ElectraNet.Domain.Commons;
using ElectraNet.Domain.Enums;

namespace ElectraNet.Domain.Enitites.Commons;

public class Asset : Auditable
{
    /// <summary>
    /// Represents the name associated with a file.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Represents the path of the file.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Represents the type of the file.
    /// </summary>
    public FileType FileType { get; set; }
}
