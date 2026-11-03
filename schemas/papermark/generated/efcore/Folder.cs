#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Folder
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string? ParentId { get; set; }

    public bool HiddenInAllDocuments { get; set; } = false;

    public string? Icon { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Folder> ChildFolders { get; set; } = new List<Folder>();

    public virtual Folder? ParentFolder { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
