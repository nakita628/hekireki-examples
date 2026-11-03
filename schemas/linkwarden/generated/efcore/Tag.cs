#nullable enable

using System;
using System.Collections.Generic;

namespace Linkwarden.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual User Owner { get; set; } = null!;

    public int OwnerId { get; set; }

    public bool? ArchiveAsScreenshot { get; set; }

    public bool? ArchiveAsMonolith { get; set; }

    public bool? ArchiveAsPDF { get; set; }

    public bool? ArchiveAsReadable { get; set; }

    public bool? ArchiveAsWaybackMachine { get; set; }

    public bool? AiTag { get; set; }

    public bool AiGenerated { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
