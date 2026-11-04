#nullable enable

using System;
using System.Collections.Generic;

namespace AnythingLLM.Models;

public partial class ModelRouters
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string FallbackProvider { get; set; } = null!;

    public string FallbackModel { get; set; } = null!;

    public int CooldownSeconds { get; set; } = 30;

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual ICollection<ModelRouterRules> Rules { get; set; } = new List<ModelRouterRules>();
}
