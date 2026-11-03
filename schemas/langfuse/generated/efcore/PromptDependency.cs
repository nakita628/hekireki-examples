#nullable enable

using System;

namespace Langfuse.Models;

public partial class PromptDependency
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ParentId { get; set; } = null!;

    public virtual Prompt Parent { get; set; } = null!;

    public string ChildName { get; set; } = null!;

    public string? ChildLabel { get; set; }

    public int? ChildVersion { get; set; }
}
