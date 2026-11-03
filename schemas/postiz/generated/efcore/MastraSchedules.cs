#nullable enable

namespace Postiz.Models;

public partial class MastraSchedules
{
    public string Id { get; set; } = null!;

    public string Target { get; set; } = null!;

    public string Cron { get; set; } = null!;

    public string? Timezone { get; set; }

    public string Status { get; set; } = null!;

    public long NextFireAt { get; set; }

    public long? LastFireAt { get; set; }

    public string? LastRunId { get; set; }

    public long CreatedAt { get; set; }

    public long UpdatedAt { get; set; }

    public string? Metadata { get; set; }

    public string? OwnerType { get; set; }

    public string? OwnerId { get; set; }
}
