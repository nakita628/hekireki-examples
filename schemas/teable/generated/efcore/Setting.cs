#nullable enable

using System;

namespace Teable.Models;

public partial class Setting
{
    public string Name { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }
}
