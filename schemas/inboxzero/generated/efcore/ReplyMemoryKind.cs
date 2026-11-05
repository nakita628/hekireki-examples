#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ReplyMemoryKind
{
    [PgName("FACT")]
    Fact,

    [PgName("PREFERENCE")]
    Preference,

    [PgName("PROCEDURE")]
    Procedure,
}
