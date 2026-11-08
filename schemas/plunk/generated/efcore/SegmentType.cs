#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum SegmentType
{
    [PgName("DYNAMIC")]
    Dynamic,

    [PgName("STATIC")]
    Static,
}
