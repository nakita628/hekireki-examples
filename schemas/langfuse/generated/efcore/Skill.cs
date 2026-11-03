#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Skill
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Frontmatter { get; set; } = "{}";

    public int Version { get; set; }

    public List<string>? Tags { get; set; } = new List<string>();

    public List<string>? Labels { get; set; } = new List<string>();

    public string? CommitMessage { get; set; }

    public virtual ICollection<SkillFile> Files { get; set; } = new List<SkillFile>();
}
