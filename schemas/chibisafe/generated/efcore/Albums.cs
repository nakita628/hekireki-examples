#nullable enable

using System;
using System.Collections.Generic;

namespace Chibisafe.Models;

public partial class Albums
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ZippedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public bool Nsfw { get; set; } = false;

    public string? SortOrder { get; set; }

    public virtual ICollection<Files> Files { get; set; } = new List<Files>();
}
