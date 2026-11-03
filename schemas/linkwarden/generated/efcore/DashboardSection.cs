#nullable enable

using System;

namespace Linkwarden.Models;

public partial class DashboardSection
{
    public int Id { get; set; }

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Collection? Collection { get; set; }

    public int? CollectionId { get; set; }

    public DashboardSectionType Type { get; set; }

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
