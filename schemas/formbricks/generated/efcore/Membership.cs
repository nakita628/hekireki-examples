#nullable enable

namespace Formbricks.Models;

public partial class Membership
{
    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public bool Accepted { get; set; } = false;

    public OrganizationRole Role { get; set; } = OrganizationRole.Member;
}
