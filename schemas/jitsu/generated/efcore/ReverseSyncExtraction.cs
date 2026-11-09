#nullable enable

using NpgsqlTypes;

namespace Jitsu.Models;

public enum ReverseSyncExtraction
{
    [PgName("cursor")]
    Cursor,

    [PgName("full")]
    Full,
}
