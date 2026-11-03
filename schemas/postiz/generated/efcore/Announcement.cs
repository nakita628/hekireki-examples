#nullable enable

using System;

namespace Postiz.Models;

public partial class Announcement
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public AnnouncementColor Color { get; set; } = AnnouncementColor.Info;

    public DateTime CreatedAt { get; set; }
}
