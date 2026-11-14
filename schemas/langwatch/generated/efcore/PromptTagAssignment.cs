#nullable enable

using System;

namespace LangWatch.Models;

public partial class PromptTagAssignment
{
    public string Id { get; set; } = null!;

    public string ConfigId { get; set; } = null!;

    public virtual LlmPromptConfig Config { get; set; } = null!;

    public string VersionId { get; set; } = null!;

    public virtual LlmPromptConfigVersion Version { get; set; } = null!;

    public string TagId { get; set; } = null!;

    public virtual PromptTag PromptTag { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public string? UpdatedById { get; set; }

    public virtual User? UpdatedBy { get; set; }
}
