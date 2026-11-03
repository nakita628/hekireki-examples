#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Prompt
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string Prompt1 { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Version { get; set; }

    public string Type { get; set; } = "text";

    public bool? IsActive { get; set; }

    public string Config { get; set; } = "{}";

    public List<string>? Tags { get; set; } = new List<string>();

    public List<string>? Labels { get; set; } = new List<string>();

    public string? CommitMessage { get; set; }

    public virtual ICollection<PromptDependency> PromptDependency { get; set; } = new List<PromptDependency>();
}
