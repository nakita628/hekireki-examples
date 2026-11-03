#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskRunExecutionStatus
{
    [PgName("RUN_CREATED")]
    RunCreated,

    [PgName("DELAYED")]
    Delayed,

    [PgName("QUEUED")]
    Queued,

    [PgName("QUEUED_EXECUTING")]
    QueuedExecuting,

    [PgName("PENDING_EXECUTING")]
    PendingExecuting,

    [PgName("EXECUTING")]
    Executing,

    [PgName("EXECUTING_WITH_WAITPOINTS")]
    ExecutingWithWaitpoints,

    [PgName("SUSPENDED")]
    Suspended,

    [PgName("PENDING_CANCEL")]
    PendingCancel,

    [PgName("FINISHED")]
    Finished,
}
