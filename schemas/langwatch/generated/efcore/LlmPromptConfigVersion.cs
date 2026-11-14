#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class LlmPromptConfigVersion
{
    public string Id { get; set; } = null!;

    public int Version { get; set; }

    public string? CommitMessage { get; set; }

    public string? AuthorId { get; set; }

    public virtual User? Author { get; set; }

    public string ConfigId { get; set; } = null!;

    public virtual LlmPromptConfig Config { get; set; } = null!;

    public string ConfigData { get; set; } = null!;

    public string SchemaVersion { get; set; } = null!;

    public string RuntimeParameters { get; set; } = "{}";

    public DateTime CreatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<PromptTagAssignment> VersionTags { get; set; } = new List<PromptTagAssignment>();
}
