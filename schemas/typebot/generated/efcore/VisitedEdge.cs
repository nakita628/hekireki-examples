#nullable enable

namespace Typebot.Models;

public partial class VisitedEdge
{
    public virtual Result Result { get; set; } = null!;

    public string ResultId { get; set; } = null!;

    public string EdgeId { get; set; } = null!;

    public int Index { get; set; }
}
