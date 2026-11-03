#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DataroomFolder
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string? ParentId { get; set; }

    public string? Icon { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<DataroomDocument> Documents { get; set; } = new List<DataroomDocument>();

    public virtual ICollection<DataroomFolder> ChildFolders { get; set; } = new List<DataroomFolder>();

    public virtual DataroomFolder? ParentFolder { get; set; }

    public virtual ICollection<Task> UploadTasks { get; set; } = new List<Task>();

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public int? OrderIndex { get; set; }

    public string? HierarchicalIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
