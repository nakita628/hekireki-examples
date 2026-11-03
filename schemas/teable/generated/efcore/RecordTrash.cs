#nullable enable

using System;

namespace Teable.Models;

public partial class RecordTrash
{
    public string Id { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string Snapshot { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string Reason { get; set; } = "deleted";

    public DateTime? RecordCreatedTime { get; set; }

    public string? RecordCreatedBy { get; set; }

    public DateTime? RecordLastModifiedTime { get; set; }

    public string? RecordLastModifiedBy { get; set; }

    public string? OperationId { get; set; }
}
