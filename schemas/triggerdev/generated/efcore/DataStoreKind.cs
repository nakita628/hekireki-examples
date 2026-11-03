#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum DataStoreKind
{
    [PgName("CLICKHOUSE")]
    Clickhouse,
}
