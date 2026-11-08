#nullable enable

using System;

namespace Plunk.Models;

public partial class IdempotencyKey
{
    public string Id { get; set; } = null!;

    public string Key { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string Path { get; set; } = null!;

    public int? StatusCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }
}
