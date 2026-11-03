#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskTriggerSource
{
    [PgName("STANDARD")]
    Standard,

    [PgName("SCHEDULED")]
    Scheduled,

    [PgName("AGENT")]
    Agent,

    [PgName("WEBHOOK")]
    Webhook,
}
