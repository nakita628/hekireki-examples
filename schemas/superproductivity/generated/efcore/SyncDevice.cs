#nullable enable

namespace SuperProductivity.Models;

public partial class SyncDevice
{
    public string ClientId { get; set; } = null!;

    public int UserId { get; set; }

    public string? DeviceName { get; set; }

    public string? UserAgent { get; set; }

    public string? AppVersion { get; set; }

    public long LastSeenAt { get; set; }

    public int LastAckedSeq { get; set; } = 0;

    public long CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
