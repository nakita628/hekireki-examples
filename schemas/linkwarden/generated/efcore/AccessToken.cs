#nullable enable

using System;

namespace Linkwarden.Models;

public partial class AccessToken
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public bool Revoked { get; set; } = false;

    public bool IsSession { get; set; } = false;

    public DateTime Expires { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
