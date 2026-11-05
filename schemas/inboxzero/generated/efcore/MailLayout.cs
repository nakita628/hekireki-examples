#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MailLayout
{
    [PgName("LIST")]
    List,

    [PgName("SPLIT")]
    Split,
}
