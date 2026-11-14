#nullable enable

namespace Langfuse.Models;

public partial class SessionViewPinOverride
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string? ViewId { get; set; }

    public virtual SessionView? View { get; set; }

    public bool IsPinned { get; set; } = true;

    public int Position { get; set; }
}
