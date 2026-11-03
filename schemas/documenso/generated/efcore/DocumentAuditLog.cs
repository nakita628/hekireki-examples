#nullable enable

using System;

namespace Documenso.Models;

public partial class DocumentAuditLog
{
    public string Id { get; set; } = null!;

    public string? EnvelopeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Type { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? UserId { get; set; }

    public string? UserAgent { get; set; }

    public string? IpAddress { get; set; }

    public virtual Envelope? Envelope { get; set; }
}
