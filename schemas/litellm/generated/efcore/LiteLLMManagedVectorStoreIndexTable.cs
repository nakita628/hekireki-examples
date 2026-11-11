#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMManagedVectorStoreIndexTable
{
    public string Id { get; set; } = null!;

    public string IndexName { get; set; } = null!;

    public string LitellmParams { get; set; } = null!;

    public string? IndexInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
