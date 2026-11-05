#nullable enable

using System;

namespace OpenPanel.Models;

public partial class ShareReport
{
    public string Id { get; set; } = null!;

    public Guid ReportId { get; set; }

    public virtual Report Report { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public bool Public { get; set; } = false;

    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
