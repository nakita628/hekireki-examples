#nullable enable

using System;

namespace Papermark.Models;

public partial class ViewerGroupAccessControls
{
    public string Id { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public virtual ViewerGroup Group { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public ItemType ItemType { get; set; }

    public bool CanView { get; set; } = true;

    public bool CanDownload { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
