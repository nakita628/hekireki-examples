#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class TopicClusteringRun
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string FacetId { get; set; } = null!;

    public int FacetVersion { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual EvaluatorVersion Facet { get; set; } = null!;

    public string Status { get; set; } = "pending";

    public string Config { get; set; } = "{}";

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public List<string>? TopicVersionIds { get; set; } = new List<string>();
}
