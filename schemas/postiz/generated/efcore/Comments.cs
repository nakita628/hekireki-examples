#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Comments
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? DisplayName { get; set; }

    public string? ParentId { get; set; }

    public int? AnchorStart { get; set; }

    public int? AnchorEnd { get; set; }

    public string? AnchorQuote { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual Comments? Parent { get; set; }

    public virtual ICollection<Comments> Replies { get; set; } = new List<Comments>();
}
