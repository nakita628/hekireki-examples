#nullable enable

using System;
using System.Collections.Generic;

namespace Chibisafe.Models;

public partial class Tags
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public virtual ICollection<Files> Files { get; set; } = new List<Files>();
}
