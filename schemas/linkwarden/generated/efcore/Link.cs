#nullable enable

using System;
using System.Collections.Generic;

namespace Linkwarden.Models;

public partial class Link
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Type { get; set; } = "url";

    public string Description { get; set; } = "";

    public virtual ICollection<Highlight> Highlight { get; set; } = new List<Highlight>();

    public virtual ICollection<User> PinnedBy { get; set; } = new List<User>();

    public virtual User? CreatedBy { get; set; }

    public int? CreatedById { get; set; }

    public virtual Collection Collection { get; set; } = null!;

    public int CollectionId { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public string? Icon { get; set; }

    public string? IconWeight { get; set; }

    public string? Color { get; set; }

    public string? Url { get; set; }

    public string? TextContent { get; set; }

    public string? Preview { get; set; }

    public string? Image { get; set; }

    public string? Pdf { get; set; }

    public string? Readable { get; set; }

    public string? Monolith { get; set; }

    public bool ClientSide { get; set; } = false;

    public bool AiTagged { get; set; } = false;

    public string? MetaDescription { get; set; }

    public int? IndexVersion { get; set; }

    public DateTime? LastPreserved { get; set; }

    public DateTime? ImportDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
