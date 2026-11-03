#nullable enable

using System;

namespace Papermark.Models;

public partial class Reaction
{
    public string Id { get; set; } = null!;

    public virtual View View { get; set; } = null!;

    public string ViewId { get; set; } = null!;

    public int PageNumber { get; set; }

    public string Type { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
