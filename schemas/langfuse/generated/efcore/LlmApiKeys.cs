#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class LlmApiKeys
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Provider { get; set; } = null!;

    public string Adapter { get; set; } = null!;

    public string DisplaySecretKey { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public string? BaseURL { get; set; }

    public List<string>? CustomModels { get; set; } = new List<string>();

    public bool WithDefaultModels { get; set; } = true;

    public string? ExtraHeaders { get; set; }

    public List<string>? ExtraHeaderKeys { get; set; } = new List<string>();

    public string? Config { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string? OrganizationId { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<DefaultLlmModel> DefaultLlmModel { get; set; } = new List<DefaultLlmModel>();

    public virtual ICollection<TopicConfig> TopicSummaryConfigs { get; set; } = new List<TopicConfig>();

    public virtual ICollection<TopicConfig> TopicEmbeddingConfigs { get; set; } = new List<TopicConfig>();

    public virtual ICollection<TopicConfig> TopicClusteringConfigs { get; set; } = new List<TopicConfig>();
}
