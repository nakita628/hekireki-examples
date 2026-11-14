#nullable enable

using System;

namespace LangWatch.Models;

public partial class DatasetVersion
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string DatasetId { get; set; } = null!;

    public int Version { get; set; }

    public string? Name { get; set; }

    public string Trigger { get; set; } = null!;

    public string ContentHash { get; set; } = null!;

    public string ColumnTypes { get; set; } = null!;

    public int RowCount { get; set; }

    public long SizeBytes { get; set; }

    public int ChunkCount { get; set; }

    public int? RestoredFromVersion { get; set; }

    public string? AuthorId { get; set; }

    public string? AuthorKind { get; set; }

    public DateTime? PinnedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
