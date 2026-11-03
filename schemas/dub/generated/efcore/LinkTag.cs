#nullable enable

using System;

namespace Dub.Models;

public partial class LinkTag
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Link Link { get; set; } = null!;

    public string LinkId { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;

    public string TagId { get; set; } = null!;
}
