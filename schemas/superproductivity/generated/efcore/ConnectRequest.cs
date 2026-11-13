#nullable enable

namespace SuperProductivity.Models;

public partial class ConnectRequest
{
    public string Id { get; set; } = null!;

    public string PollSecretHash { get; set; } = null!;

    public string MatchCode { get; set; } = null!;

    public string DeviceName { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Status { get; set; } = "pending";

    public int? UserId { get; set; }

    public int? TokenVersion { get; set; }

    public long ExpiresAt { get; set; }

    public virtual User? User { get; set; }
}
