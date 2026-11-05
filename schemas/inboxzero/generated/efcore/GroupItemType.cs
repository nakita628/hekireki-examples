#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum GroupItemType
{
    [PgName("FROM")]
    From,

    [PgName("SUBJECT")]
    Subject,

    [PgName("BODY")]
    Body,
}
