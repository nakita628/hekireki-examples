#nullable enable

using System;

namespace LangWatch.Models;

public partial class DepartmentMembershipHistory
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string DepartmentId { get; set; } = null!;

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public DateTime CreatedAt { get; set; }
}
