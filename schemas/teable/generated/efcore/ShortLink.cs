#nullable enable

using System;

namespace Teable.Models;

public partial class ShortLink
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? DeletedTime { get; set; }
}
