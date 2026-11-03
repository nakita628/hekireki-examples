#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class PromptVersion
{
    public string Id { get; set; } = null!;

    public int Version { get; set; }

    public virtual Prompt Prompt { get; set; } = null!;

    public string PromptId { get; set; } = null!;

    public string? TextContent { get; set; }

    public string? ChatContent { get; set; }

    public string? Model { get; set; }

    public string? Config { get; set; }

    public string Source { get; set; } = null!;

    public string? CommitMessage { get; set; }

    public string ContentHash { get; set; } = null!;

    public List<string>? Labels { get; set; } = new List<string>();

    public virtual BackgroundWorker? Worker { get; set; }

    public string? WorkerId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
}
