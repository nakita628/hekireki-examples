#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class AttachmentSource
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public AttachmentSourceType Type { get; set; }

    public string SourceId { get; set; } = null!;

    public string? SourcePath { get; set; }

    public string RuleId { get; set; } = null!;

    public virtual Rule Rule { get; set; } = null!;

    public string DriveConnectionId { get; set; } = null!;

    public virtual DriveConnection DriveConnection { get; set; } = null!;

    public virtual ICollection<AttachmentDocument> Documents { get; set; } = new List<AttachmentDocument>();
}
