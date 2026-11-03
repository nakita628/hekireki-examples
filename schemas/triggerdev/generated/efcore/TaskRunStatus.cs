#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskRunStatus
{
    [PgName("DELAYED")]
    Delayed,

    [PgName("PENDING")]
    Pending,

    [PgName("PENDING_VERSION")]
    PendingVersion,

    [PgName("WAITING_FOR_DEPLOY")]
    WaitingForDeploy,

    [PgName("DEQUEUED")]
    Dequeued,

    [PgName("EXECUTING")]
    Executing,

    [PgName("WAITING_TO_RESUME")]
    WaitingToResume,

    [PgName("RETRYING_AFTER_FAILURE")]
    RetryingAfterFailure,

    [PgName("PAUSED")]
    Paused,

    [PgName("CANCELED")]
    Canceled,

    [PgName("INTERRUPTED")]
    Interrupted,

    [PgName("COMPLETED_SUCCESSFULLY")]
    CompletedSuccessfully,

    [PgName("COMPLETED_WITH_ERRORS")]
    CompletedWithErrors,

    [PgName("SYSTEM_FAILURE")]
    SystemFailure,

    [PgName("CRASHED")]
    Crashed,

    [PgName("EXPIRED")]
    Expired,

    [PgName("TIMED_OUT")]
    TimedOut,
}
