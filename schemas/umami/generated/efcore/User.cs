#nullable enable

using System;
using System.Collections.Generic;

namespace Umami.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? LogoUrl { get; set; }

    public string? DisplayName { get; set; }

    public bool TwoFactorRequired { get; set; } = false;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Website> Websites { get; set; } = new List<Website>();

    public virtual ICollection<Website> CreatedBy { get; set; } = new List<Website>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<Pixel> Pixels { get; set; } = new List<Pixel>();

    public virtual ICollection<TeamUser> Teams { get; set; } = new List<TeamUser>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();

    public virtual TwoFactorAuth? TwoFactorAuth { get; set; }

    public virtual ICollection<TwoFactorBackupCode> TwoFactorBackupCodes { get; set; } = new List<TwoFactorBackupCode>();

    public virtual ICollection<TwoFactorOtpUsed> TwoFactorOtpUseds { get; set; } = new List<TwoFactorOtpUsed>();

    public virtual TwoFactorRateLimit? TwoFactorRateLimit { get; set; }

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
}
