#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum ReadStatus
{
    [PgName("NOT_OPENED")]
    NotOpened,

    [PgName("OPENED")]
    Opened,
}
