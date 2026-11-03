#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraMcpClients
{
    public string Id { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? ActiveVersionId { get; set; }

    public string? AuthorId { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
