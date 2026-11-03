#nullable enable

using System;

namespace Umami.Models;

public partial class TwoFactorBackupCode
{
    public string Id { get; set; } = null!;

    public Guid UserId { get; set; }

    public string CodeHash { get; set; } = null!;

    public bool Used { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
