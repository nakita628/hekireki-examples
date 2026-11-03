#nullable enable

using System;

namespace Papermark.Models;

public partial class CustomField
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public CustomFieldType Type { get; set; }

    public string Identifier { get; set; } = null!;

    public string Label { get; set; } = null!;

    public string? Placeholder { get; set; }

    public bool Required { get; set; } = false;

    public bool Disabled { get; set; } = false;

    public virtual Link Link { get; set; } = null!;

    public string LinkId { get; set; } = null!;

    public int OrderIndex { get; set; } = 0;
}
