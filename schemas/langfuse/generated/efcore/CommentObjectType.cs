#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum CommentObjectType
{
    [PgName("TRACE")]
    Trace,

    [PgName("OBSERVATION")]
    Observation,

    [PgName("SESSION")]
    Session,

    [PgName("PROMPT")]
    Prompt,
}
