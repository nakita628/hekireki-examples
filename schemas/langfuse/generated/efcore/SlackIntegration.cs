#nullable enable

using System;

namespace Langfuse.Models;

public partial class SlackIntegration
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public string BotToken { get; set; } = null!;

    public string BotUserId { get; set; } = null!;

    public string? InstallerUserId { get; set; }

    public string? InstallerSlackUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
