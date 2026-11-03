#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class PlatformOAuthClient
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public int Permissions { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public string? Logo { get; set; }

    public List<string>? RedirectUris { get; set; }

    public int OrganizationId { get; set; }

    public virtual Team Organization { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();

    public virtual ICollection<RefreshToken> RefreshToken { get; set; } = new List<RefreshToken>();

    public virtual ICollection<PlatformAuthorizationToken> AuthorizationTokens { get; set; } = new List<PlatformAuthorizationToken>();

    public virtual ICollection<Webhook> Webhook { get; set; } = new List<Webhook>();

    public string? BookingRedirectUri { get; set; }

    public string? BookingCancelRedirectUri { get; set; }

    public string? BookingRescheduleRedirectUri { get; set; }

    public bool AreEmailsEnabled { get; set; } = false;

    public bool AreDefaultEventTypesEnabled { get; set; } = true;

    public bool AreCalendarEventsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; }
}
