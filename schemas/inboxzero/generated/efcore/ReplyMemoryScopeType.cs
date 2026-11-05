#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ReplyMemoryScopeType
{
    [PgName("GLOBAL")]
    Global,

    [PgName("SENDER")]
    Sender,

    [PgName("DOMAIN")]
    Domain,

    [PgName("TOPIC")]
    Topic,
}
