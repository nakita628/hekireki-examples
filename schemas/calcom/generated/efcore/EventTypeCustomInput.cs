#nullable enable

namespace Calcom.Models;

public partial class EventTypeCustomInput
{
    public int Id { get; set; }

    public int EventTypeId { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public string Label { get; set; } = null!;

    public EventTypeCustomInputType Type { get; set; }

    public string? Options { get; set; }

    public bool Required { get; set; }

    public string Placeholder { get; set; } = "";
}
