#nullable enable

using System;

namespace Langfuse.Models;

public partial class TopicConfig
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual User? UpdatedByUser { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public DateTime? BlockedAt { get; set; }

    public EvaluatorBlockReason? BlockReason { get; set; }

    public string? BlockMessage { get; set; }

    public string? SummaryLlmApiKeyId { get; set; }

    public virtual LlmApiKeys? SummaryLlmApiKey { get; set; }

    public string? SummaryModel { get; set; }

    public string? EmbeddingLlmApiKeyId { get; set; }

    public virtual LlmApiKeys? EmbeddingLlmApiKey { get; set; }

    public string? EmbeddingModel { get; set; }

    public int EmbeddingDimensions { get; set; } = 1024;

    public string? ClusteringLlmApiKeyId { get; set; }

    public virtual LlmApiKeys? ClusteringLlmApiKey { get; set; }

    public string? ClusteringModel { get; set; }
}
