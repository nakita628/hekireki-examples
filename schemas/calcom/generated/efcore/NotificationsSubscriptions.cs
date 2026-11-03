#nullable enable

namespace Calcom.Models;

public partial class NotificationsSubscriptions
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public string Subscription { get; set; } = null!;
}
