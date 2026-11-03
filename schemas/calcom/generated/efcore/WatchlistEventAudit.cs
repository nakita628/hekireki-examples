#nullable enable

using System;

namespace Calcom.Models;

public partial class WatchlistEventAudit
{
    public Guid Id { get; set; }

    public Guid WatchlistId { get; set; }

    public int EventTypeId { get; set; }

    public WatchlistAction ActionTaken { get; set; }

    public DateTime Timestamp { get; set; }
}
