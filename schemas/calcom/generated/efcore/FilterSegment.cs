#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class FilterSegment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string TableIdentifier { get; set; } = null!;

    public FilterSegmentScope Scope { get; set; }

    public string? ActiveFilters { get; set; }

    public string? Sorting { get; set; }

    public string? ColumnVisibility { get; set; }

    public string? ColumnSizing { get; set; }

    public int PerPage { get; set; }

    public string? SearchTerm { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Team? Team { get; set; }

    public int? TeamId { get; set; }

    public virtual ICollection<UserFilterSegmentPreference> UserPreferences { get; set; } = new List<UserFilterSegmentPreference>();
}
