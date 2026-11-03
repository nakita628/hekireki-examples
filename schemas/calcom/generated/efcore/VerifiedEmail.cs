#nullable enable

namespace Calcom.Models;

public partial class VerifiedEmail
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string Email { get; set; } = null!;
}
