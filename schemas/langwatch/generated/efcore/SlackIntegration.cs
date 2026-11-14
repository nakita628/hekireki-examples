#nullable enable

using System;

namespace LangWatch.Models;

public partial class SlackIntegration
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public SlackIntegrationKind Kind { get; set; }

    public SlackIntegrationScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string? BotTokenEncrypted { get; set; }

    public string? WebhookUrlEncrypted { get; set; }

    public string SecretFingerprint { get; set; } = null!;

    public string SecretHint { get; set; } = null!;

    public string? SlackTeamId { get; set; }

    public string? SlackTeamName { get; set; }

    public string CreatedById { get; set; } = null!;

    public string UpdatedById { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
