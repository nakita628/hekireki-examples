#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum AuditActorType
{
    [PgName("user")]
    User,

    [PgName("guest")]
    Guest,

    [PgName("attendee")]
    Attendee,

    [PgName("system")]
    System,

    [PgName("app")]
    App,
}
