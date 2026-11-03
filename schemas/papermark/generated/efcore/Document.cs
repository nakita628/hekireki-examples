#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Document
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string File { get; set; } = null!;

    public string? OriginalFile { get; set; }

    public string? Type { get; set; }

    public string? ContentType { get; set; }

    public DocumentStorageType StorageType { get; set; } = DocumentStorageType.VercelBlob;

    public int? NumPages { get; set; }

    public virtual User? Owner { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string? OwnerId { get; set; }

    public bool AssistantEnabled { get; set; } = false;

    public bool AdvancedExcelEnabled { get; set; } = false;

    public bool AgentsEnabled { get; set; } = false;

    public bool DownloadOnly { get; set; } = false;

    public bool HiddenInAllDocuments { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<View> Views { get; set; } = new List<View>();

    public virtual ICollection<DocumentVersion> Versions { get; set; } = new List<DocumentVersion>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public string? FolderId { get; set; }

    public virtual Folder? Folder { get; set; }

    public virtual ICollection<DataroomDocument> Datarooms { get; set; } = new List<DataroomDocument>();

    public virtual ICollection<TagItem> Tags { get; set; } = new List<TagItem>();

    public virtual ICollection<DocumentAnnotation> Annotations { get; set; } = new List<DocumentAnnotation>();

    public virtual ICollection<DocumentRedactionJob> RedactionJobs { get; set; } = new List<DocumentRedactionJob>();

    public virtual ICollection<DocumentUpload> UploadedDocument { get; set; } = new List<DocumentUpload>();

    public bool IsExternalUpload { get; set; } = false;
}
