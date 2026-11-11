#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMModelTable
{
    public int Id { get; set; }

    public string? ModelAliases { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual LiteLLMTeamTable? Team { get; set; }
}
