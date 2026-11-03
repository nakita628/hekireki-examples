#nullable enable

using System;
using System.Collections.Generic;

namespace Hoppscotch.Models;

public partial class TeamCollection
{
    public string Id { get; set; } = null!;

    public string? ParentID { get; set; }

    public string TeamID { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int OrderIndex { get; set; }

    public string? Data { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual TeamCollection? Parent { get; set; }

    public virtual ICollection<TeamCollection> Children { get; set; } = new List<TeamCollection>();

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<TeamRequest> Requests { get; set; } = new List<TeamRequest>();
}
