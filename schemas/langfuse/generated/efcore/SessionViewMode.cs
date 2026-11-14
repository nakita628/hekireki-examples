#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum SessionViewMode
{
    [PgName("conversational")]
    Conversational,

    [PgName("list")]
    List,
}
