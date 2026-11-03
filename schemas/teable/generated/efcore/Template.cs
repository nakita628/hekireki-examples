#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class Template
{
    public string Id { get; set; } = null!;

    public string? BaseId { get; set; }

    public string? Cover { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? MarkdownDescription { get; set; }

    public List<string>? CategoryId { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }

    public bool? IsSystem { get; set; }

    public bool? IsPublished { get; set; }

    public bool? Featured { get; set; }

    public string? Snapshot { get; set; }

    public double Order { get; set; }

    public int UsageCount { get; set; } = 0;

    public string? PublishInfo { get; set; }

    public int VisitCount { get; set; } = 0;

    public string? Kind { get; set; }
}
