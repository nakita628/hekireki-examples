#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Dataset
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string ColumnTypes { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public virtual ICollection<DatasetRecord> DatasetRecords { get; set; } = new List<DatasetRecord>();

    public virtual ICollection<BatchEvaluation> BatchEvaluations { get; set; } = new List<BatchEvaluation>();

    public string? Mapping { get; set; }

    public bool UseS3 { get; set; } = false;

    public int? S3RecordCount { get; set; }

    public string ContentLayout { get; set; } = "postgres";

    public string Status { get; set; } = "ready";

    public string? StatusError { get; set; }

    public string? StagingKey { get; set; }

    public string? SourceStoredObjectId { get; set; }

    public string? UploadFilename { get; set; }

    public int? RowCount { get; set; }

    public long? SizeBytes { get; set; }

    public int? ChunkCount { get; set; }

    public string? ChunkOffsets { get; set; }
}
