#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class Prompt
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = "text";

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public string? FilePath { get; set; }

    public string? ExportName { get; set; }

    public string? VariableSchema { get; set; }

    public string? DefaultModel { get; set; }

    public string? DefaultConfig { get; set; }

    public List<string>? Tags { get; set; } = new List<string>();

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<PromptVersion> Versions { get; set; } = new List<PromptVersion>();
}
