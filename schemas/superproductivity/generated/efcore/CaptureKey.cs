#nullable enable

namespace SuperProductivity.Models;

public partial class CaptureKey
{
    public int UserId { get; set; }

    public string PublicKey { get; set; } = null!;

    public string Fingerprint { get; set; } = null!;

    public long CreatedAt { get; set; }

    public long? DisabledAt { get; set; }

    public virtual User User { get; set; } = null!;
}
