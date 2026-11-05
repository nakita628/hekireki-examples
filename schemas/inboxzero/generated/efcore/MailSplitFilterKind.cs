#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MailSplitFilterKind
{
    [PgName("UNREAD")]
    Unread,

    [PgName("STARRED")]
    Starred,

    [PgName("LABEL")]
    Label,

    [PgName("CATEGORY")]
    Category,

    [PgName("FROM")]
    From,

    [PgName("OLDER_THAN")]
    OlderThan,
}
