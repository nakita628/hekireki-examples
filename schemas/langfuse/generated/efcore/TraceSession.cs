#nullable enable

using System;

namespace Langfuse.Models;

public partial class TraceSession
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public bool Bookmarked { get; set; } = false;

    public bool Public { get; set; } = false;

    public string Environment { get; set; } = "default";
}
