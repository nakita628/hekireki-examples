#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class TeamRequest
{
    public string Id { get; set; } = null!;

    public string CollectionID { get; set; } = null!;

    public string TeamID { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Request { get; set; } = null!;

    public string? MockExamples { get; set; }

    public int OrderIndex { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual TeamCollection Collection { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
