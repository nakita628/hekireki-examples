#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum SendStatus
{
    [PgName("NOT_SENT")]
    NotSent,

    [PgName("SENT")]
    Sent,
}
