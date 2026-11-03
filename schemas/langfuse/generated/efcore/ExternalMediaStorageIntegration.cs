#nullable enable

using System;

namespace Langfuse.Models;

public partial class ExternalMediaStorageIntegration
{
    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public ExternalMediaStorageIntegrationType Type { get; set; }

    public string BucketName { get; set; } = null!;

    public string? Prefix { get; set; }

    public string? AccessKeyId { get; set; }

    public string? SecretAccessKey { get; set; }

    public string Region { get; set; } = null!;

    public string? Endpoint { get; set; }

    public bool ForcePathStyle { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
