#nullable enable

using System;
using System.Collections.Generic;

namespace Blinko.Models;

public partial class Comments
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int? AccountId { get; set; }

    public string? GuestName { get; set; }

    public string? GuestIP { get; set; }

    public string? GuestUA { get; set; }

    public int NoteId { get; set; }

    public int? ParentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Notes Note { get; set; } = null!;

    public virtual Accounts? Account { get; set; }

    public virtual Comments? Parent { get; set; }

    public virtual ICollection<Comments> Replies { get; set; } = new List<Comments>();
}
