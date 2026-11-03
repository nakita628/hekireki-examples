#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class Folder
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ProjectId { get; set; } = null!;

    public FolderType Type { get; set; } = FolderType.Default;

    public FolderAccessLevel? AccessLevel { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<FolderUser> Users { get; set; } = new List<FolderUser>();

    public virtual ICollection<FolderAccessRequest> AccessRequests { get; set; } = new List<FolderAccessRequest>();

    public virtual ICollection<FolderWebhook> Webhooks { get; set; } = new List<FolderWebhook>();

    public virtual Dashboard? Dashboard { get; set; }
}
