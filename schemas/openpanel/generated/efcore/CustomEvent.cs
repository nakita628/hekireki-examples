#nullable enable

using System;

namespace OpenPanel.Models;

public partial class CustomEvent
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Items { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
