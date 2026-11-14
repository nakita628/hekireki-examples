#nullable enable

using System;

namespace LangWatch.Models;

public partial class OrganizationUser
{
    public string UserId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public OrganizationUserRole Role { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? DepartmentId { get; set; }

    public DateTime? DisabledAt { get; set; }

    public string? PendingSsoGrantId { get; set; }

    public string MembershipStamp { get; set; } = null!;

    public DateTime? FullSeatSince { get; set; }

    public DateTime? SeatPendingSince { get; set; }
}
