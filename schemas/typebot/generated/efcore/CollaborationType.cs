#nullable enable

using NpgsqlTypes;

namespace Typebot.Models;

public enum CollaborationType
{
    [PgName("READ")]
    Read,

    [PgName("WRITE")]
    Write,

    [PgName("FULL_ACCESS")]
    FullAccess,
}
