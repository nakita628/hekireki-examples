#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DocumentRedactionJob
{
    public string Id { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;

    public string DocumentVersionId { get; set; } = null!;

    public virtual DocumentVersion DocumentVersion { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string CreatedById { get; set; } = null!;

    public virtual User CreatedBy { get; set; } = null!;

    public string Status { get; set; } = "PENDING";

    public List<string>? CustomTerms { get; set; }

    public List<string>? Reasons { get; set; }

    public string? ResultVersionId { get; set; }

    public string? Error { get; set; }

    public string? DetectRunId { get; set; }

    public string? ApplyRunId { get; set; }

    public virtual ICollection<DocumentRedaction> Redactions { get; set; } = new List<DocumentRedaction>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
