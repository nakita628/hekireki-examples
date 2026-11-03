#nullable enable

using System;

namespace Documenso.Models;

public partial class TeamEmailVerification
{
    public int TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool Completed { get; set; } = false;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Team Team { get; set; } = null!;
}
