#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class SlashCommandPresets
{
    public int Id { get; set; }

    public string Command { get; set; } = null!;

    public string Prompt { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Uid { get; set; } = 0;

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual Users? User { get; set; }
}
