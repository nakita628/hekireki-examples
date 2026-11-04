#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class SystemPromptVariables
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string? Value { get; set; }

    public string? Description { get; set; }

    public string Type { get; set; } = "system";

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Users? User { get; set; }
}
