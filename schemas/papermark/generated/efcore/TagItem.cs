#nullable enable

using System;

namespace Papermark.Models;

public partial class TagItem
{
    public string Id { get; set; } = null!;

    public string TagId { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;

    public TagType ItemType { get; set; }

    public string? LinkId { get; set; }

    public virtual Link? Link { get; set; }

    public string? DocumentId { get; set; }

    public virtual Document? Document { get; set; }

    public string? DataroomId { get; set; }

    public virtual Dataroom? Dataroom { get; set; }

    public string? TaggedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
