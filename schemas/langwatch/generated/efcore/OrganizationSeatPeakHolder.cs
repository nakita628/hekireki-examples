#nullable enable

namespace LangWatch.Models;

public partial class OrganizationSeatPeakHolder
{
    public string OrganizationId { get; set; } = null!;

    public string HolderKind { get; set; } = null!;

    public string HolderId { get; set; } = null!;

    public string Seat { get; set; } = null!;

    public double LastChangedAt { get; set; }

    public string? LastChangeId { get; set; }

    public double? ExpiresAt { get; set; }

    public bool ExpiredByFold { get; set; } = false;

    public string? SuppressedBy { get; set; }

    public bool RestoredByFold { get; set; } = false;
}
