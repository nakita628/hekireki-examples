#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class Folder
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public bool Pinned { get; set; } = false;

    public string? ParentId { get; set; }

    public virtual Folder? Parent { get; set; }

    public virtual ICollection<Envelope> Envelopes { get; set; } = new List<Envelope>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Folder> Subfolders { get; set; } = new List<Folder>();

    public DocumentVisibility Visibility { get; set; } = DocumentVisibility.Everyone;

    public FolderType Type { get; set; }
}
