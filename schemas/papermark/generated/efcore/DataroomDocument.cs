#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DataroomDocument
{
    public string Id { get; set; } = null!;

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;

    public string? FolderId { get; set; }

    public virtual DataroomFolder? Folder { get; set; }

    public int? OrderIndex { get; set; }

    public string? HierarchicalIndex { get; set; }

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<DataroomQuestion> Questions { get; set; } = new List<DataroomQuestion>();

    public virtual ICollection<DataroomFaqItem> DataroomFAQs { get; set; } = new List<DataroomFaqItem>();

    public virtual ICollection<DocumentUpload> UploadedDocuments { get; set; } = new List<DocumentUpload>();

    public string? VectorStoreFileId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
