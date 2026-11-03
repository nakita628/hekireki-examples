#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Tags
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<TagsPosts> Posts { get; set; } = new List<TagsPosts>();
}
