#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class InstalledIntegration
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Credentials { get; set; }

    public string? Settings { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Integration Integration { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<OAuthRefreshToken> RefreshTokens { get; set; } = new List<OAuthRefreshToken>();

    public virtual ICollection<RestrictedToken> AccessTokens { get; set; } = new List<RestrictedToken>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();
}
