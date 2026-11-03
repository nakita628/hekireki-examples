#nullable enable

using System;

namespace Trigger.Models;

public partial class PlaygroundConversation
{
    public string Id { get; set; } = null!;

    public string ChatId { get; set; } = null!;

    public string Title { get; set; } = "New conversation";

    public string AgentSlug { get; set; } = null!;

    public string? RunId { get; set; }

    public virtual TaskRun? Run { get; set; }

    public string? ClientData { get; set; }

    public string? Messages { get; set; }

    public string? LastEventId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
