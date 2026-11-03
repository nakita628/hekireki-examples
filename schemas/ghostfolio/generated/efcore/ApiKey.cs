#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class ApiKey
{
    public DateTime CreatedAt { get; set; }

    public string HashedKey { get; set; } = null!;

    public string Id { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
