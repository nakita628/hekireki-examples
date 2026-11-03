#nullable enable

using System;

namespace Langfuse.Models;

public partial class PendingDeletion
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Object { get; set; } = null!;

    public string ObjectId { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
