#nullable enable

using System;

namespace OpenPanel.Models;

public partial class EventMeta
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Conversion { get; set; }

    public string? Color { get; set; }

    public string? Icon { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
