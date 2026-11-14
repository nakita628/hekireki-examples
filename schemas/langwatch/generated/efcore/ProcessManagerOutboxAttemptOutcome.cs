#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ProcessManagerOutboxAttemptOutcome
{
    [PgName("retry_scheduled")]
    RetryScheduled,

    [PgName("dead")]
    Dead,
}
