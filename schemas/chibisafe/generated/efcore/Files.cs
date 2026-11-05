#nullable enable

using System;
using System.Collections.Generic;

namespace Chibisafe.Models;

public partial class Files
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Original { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Size { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public string? SourceUrl { get; set; }

    public bool IsS3 { get; set; } = false;

    public bool IsWatched { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public virtual Users? User { get; set; }

    public virtual ICollection<Albums> Albums { get; set; } = new List<Albums>();

    public virtual ICollection<Tags> Tags { get; set; } = new List<Tags>();

    public bool Quarantine { get; set; } = false;

    public virtual QuarantineFiles? QuarantineFile { get; set; }
}
