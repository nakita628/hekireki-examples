#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class EvalTemplate
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string Name { get; set; } = null!;

    public int Version { get; set; }

    public string? Prompt { get; set; }

    public EvalTemplateType Type { get; set; } = EvalTemplateType.LlmAsJudge;

    public string? Partner { get; set; }

    public string? Model { get; set; }

    public string? Provider { get; set; }

    public string? ModelParams { get; set; }

    public List<string>? Vars { get; set; } = new List<string>();

    public string? OutputDefinition { get; set; }

    public string? SourceCode { get; set; }

    public EvalTemplateSourceCodeLanguage? SourceCodeLanguage { get; set; }

    public virtual ICollection<JobConfiguration> JobConfiguration { get; set; } = new List<JobConfiguration>();
}
