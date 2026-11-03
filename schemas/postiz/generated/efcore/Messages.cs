#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Messages
{
    public string Id { get; set; } = null!;

    public From From { get; set; }

    public string? Content { get; set; }

    public string GroupId { get; set; } = null!;

    public string? Special { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual MessagesGroup Group { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
