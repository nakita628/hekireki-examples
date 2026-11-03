#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum RedirectType
{
    [PgName("user-event-type")]
    UserEventType,

    [PgName("team-event-type")]
    TeamEventType,

    [PgName("user")]
    User,

    [PgName("team")]
    Team,
}
