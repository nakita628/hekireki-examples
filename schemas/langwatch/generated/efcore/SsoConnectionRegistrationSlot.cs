#nullable enable

using System;

namespace LangWatch.Models;

public partial class SsoConnectionRegistrationSlot
{
    public string OrganizationId { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string? ReplacesConnectionId { get; set; }

    public string CommandId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
