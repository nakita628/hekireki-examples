#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MeetingJoinRule
{
    [PgName("ALL")]
    All,

    [PgName("EXTERNAL_ONLY")]
    ExternalOnly,

    [PgName("HOST_ONLY")]
    HostOnly,

    [PgName("OFF")]
    Off,
}
