#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraThreads
{
    public string Id { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
