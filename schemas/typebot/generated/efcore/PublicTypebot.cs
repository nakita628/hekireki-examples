#nullable enable

using System;
using System.Collections.Generic;

namespace Typebot.Models;

public partial class PublicTypebot
{
    public string Id { get; set; } = null!;

    public string? Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastActivityAt { get; set; }

    public string TypebotId { get; set; } = null!;

    public string Groups { get; set; } = null!;

    public string? Events { get; set; }

    public string Variables { get; set; } = null!;

    public string Edges { get; set; } = null!;

    public string Theme { get; set; } = null!;

    public string Settings { get; set; } = null!;

    public virtual Typebot Typebot { get; set; } = null!;

    public virtual ICollection<RuntimeMediaIdCache> MediaIdsCache { get; set; } = new List<RuntimeMediaIdCache>();
}
