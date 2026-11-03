#nullable enable

using System;

namespace Dub.Models;

public partial class Token
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string HashedKey { get; set; } = null!;

    public string PartialKey { get; set; } = null!;

    public DateTime? Expires { get; set; }

    public DateTime? LastUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
