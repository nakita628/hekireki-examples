#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class LlmPromptConfig
{
    public string Id { get; set; } = null!;

    public string? Handle { get; set; }

    public string Name { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string OrganizationId { get; set; } = null!;

    public PromptScope Scope { get; set; } = PromptScope.Project;

    public string? CopiedFromPromptId { get; set; }

    public virtual LlmPromptConfig? CopiedFrom { get; set; }

    public virtual ICollection<LlmPromptConfig> CopiedPrompts { get; set; } = new List<LlmPromptConfig>();

    public virtual ICollection<LlmPromptConfigVersion> Versions { get; set; } = new List<LlmPromptConfigVersion>();

    public virtual ICollection<PromptTagAssignment> VersionTags { get; set; } = new List<PromptTagAssignment>();
}
