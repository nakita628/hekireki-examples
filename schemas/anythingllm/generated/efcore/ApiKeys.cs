#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class ApiKeys
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Secret { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
