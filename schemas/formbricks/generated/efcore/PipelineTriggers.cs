#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum PipelineTriggers
{
    [PgName("responseCreated")]
    ResponseCreated,

    [PgName("responseUpdated")]
    ResponseUpdated,

    [PgName("responseFinished")]
    ResponseFinished,
}
