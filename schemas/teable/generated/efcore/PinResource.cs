#nullable enable

using System;

namespace Teable.Models;

public partial class PinResource
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public double Order { get; set; }
}
