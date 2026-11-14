#nullable enable

using System;

namespace LangWatch.Models;

public partial class TraceIngestSourceBilling
{
    public string OrganizationId { get; set; } = null!;

    public string SourceType { get; set; } = null!;

    public bool Billed { get; set; }

    public DateTime RecordedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
