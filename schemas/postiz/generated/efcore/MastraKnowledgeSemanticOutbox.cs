#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraKnowledgeSemanticOutbox
{
    public string Id { get; set; } = null!;

    public string IdempotencyKey { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string Operation { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string ScopeKey { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Attempts { get; set; }

    public DateTime AvailableAt { get; set; }

    public DateTime? ClaimedAt { get; set; }

    public string? ClaimedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? AvailableAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? ClaimedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAtZ { get; set; } = DateTime.UtcNow;
}
