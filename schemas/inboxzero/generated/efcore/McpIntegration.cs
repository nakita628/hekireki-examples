#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class McpIntegration
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? RegisteredServerUrl { get; set; }

    public string? RegisteredAuthorizationUrl { get; set; }

    public string? RegisteredTokenUrl { get; set; }

    public string? OauthClientId { get; set; }

    public string? OauthClientSecret { get; set; }

    public string? DisplayName { get; set; }

    public string? ServerUrl { get; set; }

    public McpAuthType? AuthType { get; set; }

    public string? EmailAccountId { get; set; }

    public virtual EmailAccount? EmailAccount { get; set; }

    public virtual ICollection<McpConnection> Connections { get; set; } = new List<McpConnection>();
}
