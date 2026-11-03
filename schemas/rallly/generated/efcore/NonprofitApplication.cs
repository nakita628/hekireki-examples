#nullable enable

using System;

namespace Rallly.Models;

public partial class NonprofitApplication
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string EmailDomain { get; set; } = null!;

    public NonprofitApplicationStatus Status { get; set; }

    public string? Reason { get; set; }

    public string? Model { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
