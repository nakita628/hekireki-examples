#nullable enable

using System;

namespace Teable.Models;

public partial class TemplateCategory
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }

    public double Order { get; set; }
}
