#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class PersonalAccessToken
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string EncryptedToken { get; set; } = null!;

    public string ObfuscatedToken { get; set; } = null!;

    public string HashedToken { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime? RevokedAt { get; set; }

    public DateTime? LastAccessedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int? IdleTimeoutSeconds { get; set; }

    public DateTime? ExpiryReminderSentAt { get; set; }

    public DateTime? ExpiredNoticeSentAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AuthorizationCode> AuthorizationCodes { get; set; } = new List<AuthorizationCode>();
}
