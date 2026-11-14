#nullable enable

namespace LangWatch.Models;

public partial class OrganizationSeatPeak
{
    public string OrganizationId { get; set; } = null!;

    public double OpenedAt { get; set; }

    public int FullSeats { get; set; }

    public int LiteSeats { get; set; }

    public int DeveloperSeats { get; set; }

    public double LatestChangeAt { get; set; }

    public int Mismatches { get; set; }
}
