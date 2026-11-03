#nullable enable

using System;

namespace Documenso.Models;

public partial class OrganisationMonthlyStat
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public string Period { get; set; } = null!;

    public int DocumentCount { get; set; } = 0;

    public int EmailCount { get; set; } = 0;

    public int ApiCount { get; set; } = 0;

    public int EmailReports { get; set; } = 0;
}
