#nullable enable

using System;

namespace LangWatch.Models;

public partial class NurturingOrganizationMember
{
    public string OrganizationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime? JoinedAt { get; set; }

    public DateTime? LeftAt { get; set; }

    public DateTime? ReturnedAt { get; set; }
}
