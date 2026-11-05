#nullable enable

namespace Chibisafe.Models;

public partial class QuarantineFiles
{
    public string FileId { get; set; } = null!;

    public virtual Files File { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Reason { get; set; }
}
