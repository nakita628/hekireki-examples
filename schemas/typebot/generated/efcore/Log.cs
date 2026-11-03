#nullable enable

using System;

namespace Typebot.Models;

public partial class Log
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string ResultId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Context { get; set; }

    public string Description { get; set; } = null!;

    public string? Details { get; set; }

    public virtual Result Result { get; set; } = null!;
}
