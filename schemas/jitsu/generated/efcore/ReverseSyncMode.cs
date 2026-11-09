#nullable enable

using NpgsqlTypes;

namespace Jitsu.Models;

public enum ReverseSyncMode
{
    [PgName("upsert")]
    Upsert,

    [PgName("mirror")]
    Mirror,
}
