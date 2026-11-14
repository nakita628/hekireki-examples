#nullable enable

using System;

namespace LangWatch.Models;

public partial class SsoActivationRecoveryReservation
{
    public string CommandId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
