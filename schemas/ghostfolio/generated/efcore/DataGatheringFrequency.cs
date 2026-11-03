#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum DataGatheringFrequency
{
    [PgName("DAILY")]
    Daily,

    [PgName("HOURLY")]
    Hourly,
}
