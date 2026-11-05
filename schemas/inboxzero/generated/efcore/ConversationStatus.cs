#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ConversationStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("STOPPED")]
    Stopped,
}
