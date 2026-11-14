#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum SessionUnmatchedTraces
{
    [PgName("show")]
    Show,

    [PgName("hide")]
    Hide,
}
