#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraDatasetVersions
{
    public string Id { get; set; } = null!;

    public string DatasetId { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
