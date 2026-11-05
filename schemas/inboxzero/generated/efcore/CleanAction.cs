#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum CleanAction
{
    [PgName("ARCHIVE")]
    Archive,

    [PgName("MARK_READ")]
    MarkRead,
}
