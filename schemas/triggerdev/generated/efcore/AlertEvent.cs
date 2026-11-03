#nullable enable

using System;

namespace Trigger.Models;

public partial class AlertEvent
{
    public string Id { get; set; } = null!;

    public string AlertId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime At { get; set; }

    public string Facts { get; set; } = "{}";

    public string? ActorId { get; set; }

    public string? SourceEventId { get; set; }
}
