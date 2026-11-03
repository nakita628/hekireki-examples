#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Task
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = "TODO";

    public string Status { get; set; } = "OPEN";

    public string? Category { get; set; }

    public DateTime? DueDate { get; set; }

    public int? OrderIndex { get; set; }

    public string TaskListId { get; set; } = null!;

    public virtual TaskList TaskList { get; set; } = null!;

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string? UploadFolderId { get; set; }

    public virtual DataroomFolder? UploadFolder { get; set; }

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();

    public virtual ICollection<TaskActivity> Activities { get; set; } = new List<TaskActivity>();

    public virtual ICollection<DocumentUpload> Uploads { get; set; } = new List<DocumentUpload>();

    public DateTime? LastActivityNotifiedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
