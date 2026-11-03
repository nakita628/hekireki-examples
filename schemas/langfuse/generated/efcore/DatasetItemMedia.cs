#nullable enable

using System;

namespace Langfuse.Models;

public partial class DatasetItemMedia
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string MediaId { get; set; } = null!;

    public string DatasetId { get; set; } = null!;

    public string DatasetItemId { get; set; } = null!;

    public DateTime? DatasetItemValidFrom { get; set; }

    public string Field { get; set; } = null!;

    public string? JsonPath { get; set; }

    public string? ReferenceString { get; set; }
}
