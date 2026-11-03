#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ImpersonationAuditLogAction
{
    [PgName("START")]
    Start,

    [PgName("STOP")]
    Stop,
}
