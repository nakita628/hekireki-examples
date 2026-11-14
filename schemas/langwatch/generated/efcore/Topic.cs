#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Topic
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ParentId { get; set; }

    public virtual Topic? Parent { get; set; }

    public virtual ICollection<Topic> Subtopics { get; set; } = new List<Topic>();

    public string EmbeddingsModel { get; set; } = null!;

    public string Centroid { get; set; } = null!;

    public double P95Distance { get; set; }

    public bool AutomaticallyGenerated { get; set; } = true;

    public string? LastEventId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
