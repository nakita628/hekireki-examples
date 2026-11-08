#nullable enable

using System;

namespace Plunk.Models;

public partial class Domain
{
    public string Id { get; set; } = null!;

    public string Domain1 { get; set; } = null!;

    public bool Verified { get; set; } = false;

    public string? DkimTokens { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
