#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum RRTimestampBasis
{
    [PgName("CREATED_AT")]
    CreatedAt,

    [PgName("START_TIME")]
    StartTime,
}
