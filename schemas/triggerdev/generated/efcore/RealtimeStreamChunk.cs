#nullable enable

using System;

namespace Trigger.Models;

public partial class RealtimeStreamChunk
{
    public string Id { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int Sequence { get; set; }

    public string RunId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
