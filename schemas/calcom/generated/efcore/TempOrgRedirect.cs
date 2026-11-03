#nullable enable

using System;

namespace Calcom.Models;

public partial class TempOrgRedirect
{
    public int Id { get; set; }

    public string From { get; set; } = null!;

    public int FromOrgId { get; set; }

    public RedirectType Type { get; set; }

    public string ToUrl { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
