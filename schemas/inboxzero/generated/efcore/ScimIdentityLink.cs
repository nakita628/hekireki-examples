#nullable enable

namespace InboxZero.Models;

public partial class ScimIdentityLink
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
