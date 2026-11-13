#nullable enable

namespace SuperProductivity.Models;

public partial class CaptureConnectCode
{
    public int UserId { get; set; }

    public string CodeHash { get; set; } = null!;

    public int TokenVersion { get; set; }

    public long ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
