#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class McpConnection
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? ApiKey { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string IntegrationId { get; set; } = null!;

    public virtual McpIntegration Integration { get; set; } = null!;

    public string? EmailAccountId { get; set; }

    public virtual EmailAccount? EmailAccount { get; set; }

    public virtual ICollection<McpTool> Tools { get; set; } = new List<McpTool>();
}
