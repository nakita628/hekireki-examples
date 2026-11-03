#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum AnnotationQueueObjectType
{
    [PgName("TRACE")]
    Trace,

    [PgName("OBSERVATION")]
    Observation,

    [PgName("SESSION")]
    Session,
}
