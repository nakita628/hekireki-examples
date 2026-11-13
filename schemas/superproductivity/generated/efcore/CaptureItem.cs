#nullable enable

namespace SuperProductivity.Models;

public partial class CaptureItem
{
    public string Id { get; set; } = null!;

    public int UserId { get; set; }

    public string GrantId { get; set; } = null!;

    public string Ciphertext { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public long CreatedAt { get; set; }

    public string? LeaseDeviceId { get; set; }

    public long? LeaseUntil { get; set; }

    public virtual User User { get; set; } = null!;
}
