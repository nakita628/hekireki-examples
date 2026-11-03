#nullable enable

using System;

namespace Teable.Models;

public partial class AttachmentsTable
{
    public string Id { get; set; } = null!;

    public string AttachmentId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string FieldId { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedTime { get; set; }
}
