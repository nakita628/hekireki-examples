#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class OAuthClient
{
    public string ClientId { get; set; } = null!;

    public string RedirectUri { get; set; } = null!;

    public string? ClientSecret { get; set; }

    public OAuthClientType ClientType { get; set; } = OAuthClientType.Confidential;

    public string Name { get; set; } = null!;

    public string? Purpose { get; set; }

    public string? Logo { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? RejectionReason { get; set; }

    public bool IsTrusted { get; set; } = false;

    public virtual ICollection<AccessCode> AccessCodes { get; set; } = new List<AccessCode>();

    public OAuthClientStatus Status { get; set; } = OAuthClientStatus.Approved;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public DateTime CreatedAt { get; set; }
}
