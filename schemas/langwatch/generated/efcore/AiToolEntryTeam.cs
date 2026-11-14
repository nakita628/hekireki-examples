#nullable enable

namespace LangWatch.Models;

public partial class AiToolEntryTeam
{
    public string Id { get; set; } = null!;

    public string EntryId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual AiToolEntry Entry { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
