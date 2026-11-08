#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum CampaignAudienceType
{
    [PgName("ALL")]
    All,

    [PgName("FILTERED")]
    Filtered,

    [PgName("SEGMENT")]
    Segment,
}
