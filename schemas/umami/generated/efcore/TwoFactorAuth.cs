#nullable enable

using System;

namespace Umami.Models;

public partial class TwoFactorAuth
{
    public string Id { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Secret { get; set; } = null!;

    public bool IsEnabled { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
