#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMManagedVectorStoresTable
{
    public string VectorStoreId { get; set; } = null!;

    public string CustomLlmProvider { get; set; } = null!;

    public string? VectorStoreName { get; set; }

    public string? VectorStoreDescription { get; set; }

    public string? VectorStoreMetadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? LitellmCredentialName { get; set; }

    public string? LitellmParams { get; set; }

    public string? TeamId { get; set; }

    public string? UserId { get; set; }
}
