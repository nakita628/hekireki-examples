#nullable enable

using System;

namespace Linkwarden.Models;

public partial class Highlight
{
    public int Id { get; set; }

    public string Color { get; set; } = null!;

    public string? Comment { get; set; }

    public virtual Link Link { get; set; } = null!;

    public int LinkId { get; set; }

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public int StartOffset { get; set; }

    public int EndOffset { get; set; }

    public string Text { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
