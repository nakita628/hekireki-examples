#nullable enable

using System;

namespace Teable.Models;

public partial class BaseShare
{
    public string Id { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string ShareId { get; set; } = null!;

    public string? Password { get; set; }

    public string? NodeId { get; set; }

    public bool? AllowSave { get; set; }

    public bool? AllowCopy { get; set; }

    public bool? AllowEdit { get; set; }

    public bool Enabled { get; set; } = true;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }
}
