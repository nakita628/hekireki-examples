#nullable enable

using System;

namespace Linkwarden.Models;

public partial class RssSubscription
{
    public int Id { get; set; }

    public string Url { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? LastBuildDate { get; set; }

    public virtual Collection Collection { get; set; } = null!;

    public int CollectionId { get; set; }

    public int OwnerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
