#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class WorkspaceChats
{
    public int Id { get; set; }

    public int WorkspaceId { get; set; }

    public string Prompt { get; set; } = null!;

    public string Response { get; set; } = null!;

    public bool Include { get; set; } = true;

    public int? UserId { get; set; }

    public int? ThreadId { get; set; }

    public string? ApiSessionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool? FeedbackScore { get; set; }

    public bool? MemoryProcessed { get; set; }

    public virtual Users? Users { get; set; }
}
