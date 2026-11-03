#nullable enable

using NpgsqlTypes;

namespace Linkwarden.Models;

public enum DashboardSectionType
{
    [PgName("STATS")]
    Stats,

    [PgName("RECENT_LINKS")]
    RecentLinks,

    [PgName("PINNED_LINKS")]
    PinnedLinks,

    [PgName("COLLECTION")]
    Collection,
}
