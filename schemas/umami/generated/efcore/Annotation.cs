#nullable enable

using System;

namespace Umami.Models;

public partial class Annotation
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime Date { get; set; }

    public bool AllDay { get; set; } = true;

    public string Note { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual Website Website { get; set; } = null!;
}
