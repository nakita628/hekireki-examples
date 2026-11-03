#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class Analytics
{
    public int ActivityCount { get; set; } = 0;

    public string? Country { get; set; }

    public int DataProviderGhostfolioDailyRequests { get; set; } = 0;

    public DateTime LastRequestAt { get; set; }

    public RequestClient? LastRequestClient { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
