#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ScheduleGeneratorType
{
    [PgName("CRON")]
    Cron,
}
