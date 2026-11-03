#nullable enable

using System;

namespace Umami.Models;

public partial class SessionReplay
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public Guid SessionId { get; set; }

    public Guid VisitId { get; set; }

    public int ChunkIndex { get; set; }

    public byte[] Events { get; set; } = null!;

    public int EventCount { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime EndedAt { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Website Website { get; set; } = null!;
}
