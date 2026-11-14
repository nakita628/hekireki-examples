#nullable enable

using System;

namespace LangWatch.Models;

public partial class SlackConnectionClaim
{
    public string ConnectionId { get; set; } = null!;

    public string ClaimantId { get; set; } = null!;

    public string ClaimantLabel { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
