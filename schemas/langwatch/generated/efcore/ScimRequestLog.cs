#nullable enable

using System;

namespace LangWatch.Models;

public partial class ScimRequestLog
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string? ConnectionId { get; set; }

    public string Method { get; set; } = null!;

    public string Resource { get; set; } = null!;

    public int Status { get; set; }

    public string? Reason { get; set; }

    public string? Detail { get; set; }

    public DateTime OccurredAt { get; set; }
}
