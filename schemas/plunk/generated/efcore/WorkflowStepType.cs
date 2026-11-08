#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum WorkflowStepType
{
    [PgName("TRIGGER")]
    Trigger,

    [PgName("SEND_EMAIL")]
    SendEmail,

    [PgName("DELAY")]
    Delay,

    [PgName("WAIT_FOR_EVENT")]
    WaitForEvent,

    [PgName("CONDITION")]
    Condition,

    [PgName("EXIT")]
    Exit,

    [PgName("WEBHOOK")]
    Webhook,

    [PgName("UPDATE_CONTACT")]
    UpdateContact,
}
