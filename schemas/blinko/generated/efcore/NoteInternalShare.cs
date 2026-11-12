#nullable enable

using System;

namespace Blinko.Models;

public partial class NoteInternalShare
{
    public int Id { get; set; }

    public int NoteId { get; set; }

    public int AccountId { get; set; }

    public bool CanEdit { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Notes Note { get; set; } = null!;

    public virtual Accounts Account { get; set; } = null!;
}
