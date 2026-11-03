#nullable enable

using System;

namespace Trigger.Models;

public partial class SessionRun
{
    public string Id { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public DateTime TriggeredAt { get; set; }

    public virtual Session Session { get; set; } = null!;
}
