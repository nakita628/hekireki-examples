#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ShareResourceType
{
    [PgName("TRACE")]
    Trace,

    [PgName("THREAD")]
    Thread,
}
