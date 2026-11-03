#nullable enable

using System;

namespace Papermark.Models;

public partial class ViewerGroupMembership
{
    public string Id { get; set; } = null!;

    public string ViewerId { get; set; } = null!;

    public virtual Viewer Viewer { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public virtual ViewerGroup Group { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
