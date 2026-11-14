#nullable enable

using System;

namespace LangWatch.Models;

public partial class CustomLLMModelCost
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public CustomLLMModelCostScopeType ScopeType { get; set; } = CustomLLMModelCostScopeType.Project;

    public string ScopeId { get; set; } = null!;

    public string? ProjectId { get; set; }

    public string Model { get; set; } = null!;

    public string Regex { get; set; } = null!;

    public double? InputCostPerToken { get; set; }

    public double? OutputCostPerToken { get; set; }

    public double? CacheReadCostPerToken { get; set; }

    public double? CacheCreationCostPerToken { get; set; }

    public double? CacheCreation1hCostPerToken { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
