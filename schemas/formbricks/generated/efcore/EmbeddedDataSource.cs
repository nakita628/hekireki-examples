#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum EmbeddedDataSource
{
    [PgName("computed")]
    Computed,

    [PgName("ingested")]
    Ingested,

    [PgName("reserved")]
    Reserved,
}
