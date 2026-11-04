#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class ModelRouterRules
{
    public int Id { get; set; }

    public int RouterId { get; set; }

    public bool Enabled { get; set; } = true;

    public int Priority { get; set; }

    public string Type { get; set; } = "calculated";

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ConditionLogic { get; set; }

    public string? Conditions { get; set; }

    public string RouteProvider { get; set; } = null!;

    public string RouteModel { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual ModelRouters Router { get; set; } = null!;
}
