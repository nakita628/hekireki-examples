#nullable enable

using System;

namespace Umami.Models;

public partial class ApiKey
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string KeyHash { get; set; } = null!;

    public string KeyPrefix { get; set; } = null!;

    public DateTime? LastUsedAt { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual User User { get; set; } = null!;
}
