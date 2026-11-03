#nullable enable

using System;

namespace Dub.Models;

public partial class SentEmail
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }
}
