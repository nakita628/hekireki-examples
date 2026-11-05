#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string SessionToken { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime Expires { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool EmailOtp { get; set; } = false;

    public int EmailOtpVersion { get; set; } = 0;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public virtual User User { get; set; } = null!;

    public string? ActiveOrganizationId { get; set; }

    public virtual Organization? ActiveOrganization { get; set; }

    public virtual ICollection<OauthRefreshToken> OauthRefreshTokenRecords { get; set; } = new List<OauthRefreshToken>();

    public virtual ICollection<OauthAccessToken> OauthAccessTokenRecords { get; set; } = new List<OauthAccessToken>();
}
