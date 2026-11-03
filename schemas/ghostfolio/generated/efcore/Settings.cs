#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class Settings
{
    public string? Settings1 { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
