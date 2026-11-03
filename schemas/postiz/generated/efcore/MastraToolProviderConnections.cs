#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraToolProviderConnections
{
    public string AuthorId { get; set; } = null!;

    public string ProviderId { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string Toolkit { get; set; } = null!;

    public string? Label { get; set; }

    public string Scope { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
