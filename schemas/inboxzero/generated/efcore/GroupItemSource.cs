#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum GroupItemSource
{
    [PgName("AI")]
    Ai,

    [PgName("USER")]
    User,

    [PgName("LABEL_REMOVED")]
    LabelRemoved,

    [PgName("LABEL_ADDED")]
    LabelAdded,
}
