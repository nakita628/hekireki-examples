#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class DocumentSyncExecutions
{
    public int Id { get; set; }

    public int QueueId { get; set; }

    public string Status { get; set; } = "unknown";

    public string? Result { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual DocumentSyncQueues Queue { get; set; } = null!;
}
