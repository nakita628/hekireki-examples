#nullable enable

namespace LangWatch.Models;

public partial class ScimDirectoryUser
{
    public string OrganizationId { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
