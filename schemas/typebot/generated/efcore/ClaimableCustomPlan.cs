#nullable enable

using System;

namespace Typebot.Models;

public partial class ClaimableCustomPlan
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ClaimedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Price { get; set; }

    public string Currency { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public int ChatsLimit { get; set; }

    public int StorageLimit { get; set; }

    public int SeatsLimit { get; set; }

    public bool IsYearly { get; set; } = false;

    public string? CompanyName { get; set; }

    public string? VatType { get; set; }

    public string? VatValue { get; set; }
}
