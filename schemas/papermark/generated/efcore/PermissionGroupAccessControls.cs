#nullable enable

using System;

namespace Papermark.Models;

public partial class PermissionGroupAccessControls
{
    public string Id { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public virtual PermissionGroup Group { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public ItemType ItemType { get; set; }

    public bool CanView { get; set; } = true;

    public bool CanDownload { get; set; } = false;

    public bool CanDownloadOriginal { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
