#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class Sheet
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string HostId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string UrlId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Deleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual User Host { get; set; } = null!;

    public virtual ICollection<SheetSlot> Slots { get; set; } = new List<SheetSlot>();
}
