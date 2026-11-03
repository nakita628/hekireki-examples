#nullable enable

using System;

namespace Papermark.Models;

public partial class LinkVisitorGroup
{
    public string Id { get; set; } = null!;

    public string LinkId { get; set; } = null!;

    public virtual Link Link { get; set; } = null!;

    public string VisitorGroupId { get; set; } = null!;

    public virtual VisitorGroup VisitorGroup { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
