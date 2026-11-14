#nullable enable

using System;

namespace LangWatch.Models;

public partial class AuthzOrganizationPolicy
{
    public string OrganizationId { get; set; } = null!;

    public bool BraidsOrganizationAdminsOnly { get; set; } = false;

    public string UpdatedByUserId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
