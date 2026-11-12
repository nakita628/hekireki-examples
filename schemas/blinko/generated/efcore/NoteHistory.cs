#nullable enable

using System;

namespace Blinko.Models;

public partial class NoteHistory
{
    public int Id { get; set; }

    public int NoteId { get; set; }

    public string Content { get; set; } = null!;

    public string? Metadata { get; set; }

    public int Version { get; set; }

    public int? AccountId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Notes Note { get; set; } = null!;
}
