#nullable enable

using System;

namespace Documenso.Models;

public partial class SiteSettings
{
    public string Id { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public string Data { get; set; } = null!;

    public int? LastModifiedByUserId { get; set; }

    public DateTime LastModifiedAt { get; set; }

    public virtual User? LastModifiedByUser { get; set; }
}
