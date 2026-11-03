#nullable enable

using System;

namespace Umami.Models;

public partial class TwoFactorOtpUsed
{
    public string Id { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Otp { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
