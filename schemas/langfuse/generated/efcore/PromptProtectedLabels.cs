#nullable enable

using System;

namespace Langfuse.Models;

public partial class PromptProtectedLabels
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Label { get; set; } = null!;
}
