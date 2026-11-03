#nullable enable

using System;

namespace Langfuse.Models;

public partial class DefaultView
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string ViewName { get; set; } = null!;

    public string ViewId { get; set; } = null!;
}
