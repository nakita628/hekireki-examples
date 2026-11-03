#nullable enable

using System;

namespace Langfuse.Models;

public partial class TraceMedia
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string MediaId { get; set; } = null!;

    public string TraceId { get; set; } = null!;

    public string Field { get; set; } = null!;

    public MediaAssociationOrigin Origin { get; set; } = MediaAssociationOrigin.Unknown;
}
