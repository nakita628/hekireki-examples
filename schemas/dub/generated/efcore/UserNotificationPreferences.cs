#nullable enable

namespace Dub.Models;

public partial class UserNotificationPreferences
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public bool DubLinks { get; set; } = true;

    public virtual User User { get; set; } = null!;
}
