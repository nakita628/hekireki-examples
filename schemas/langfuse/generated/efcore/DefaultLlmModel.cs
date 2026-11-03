#nullable enable

using System;

namespace Langfuse.Models;

public partial class DefaultLlmModel
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string LlmApiKeyId { get; set; } = null!;

    public virtual LlmApiKeys LlmApiKey { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string Adapter { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? ModelParams { get; set; }
}
