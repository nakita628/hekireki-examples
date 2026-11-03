#nullable enable

using System;

namespace Postiz.Models;

public partial class TagsPosts
{
    public string PostId { get; set; } = null!;

    public string TagId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Tags Tag { get; set; } = null!;
}
