#nullable enable

namespace Calcom.Models;

public partial class VerifiedNumber
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string PhoneNumber { get; set; } = null!;
}
