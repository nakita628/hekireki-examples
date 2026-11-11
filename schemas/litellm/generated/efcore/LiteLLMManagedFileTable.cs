#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMManagedFileTable
{
    public string Id { get; set; } = null!;

    public string UnifiedFileId { get; set; } = null!;

    public string? FileObject { get; set; }

    public string ModelMappings { get; set; } = null!;

    public List<string>? FlatModelFileIds { get; set; } = new List<string>();

    public string? StorageBackend { get; set; }

    public string? StorageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? TeamId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
