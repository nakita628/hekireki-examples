#nullable enable

using System;

namespace Blinko.Models;

public partial class NoteReference
{
    public int Id { get; set; }

    public int FromNoteId { get; set; }

    public int ToNoteId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Notes FromNote { get; set; } = null!;

    public virtual Notes ToNote { get; set; } = null!;
}
