#nullable enable

using System;

namespace Rallly.Models;

public partial class ApiUsage
{
    public string SpaceId { get; set; } = null!;

    public ApiUsageResource Resource { get; set; }

    public DateOnly Period { get; set; }

    public int Count { get; set; } = 0;

    public DateTime? NearLimitWarnedAt { get; set; }

    public DateTime? AtLimitWarnedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Space Space { get; set; } = null!;
}
