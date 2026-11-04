#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class Invites
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Status { get; set; } = "pending";

    public int? ClaimedBy { get; set; }

    public string? WorkspaceIds { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
