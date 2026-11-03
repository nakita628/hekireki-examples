#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum SigningStatus
{
    [PgName("NOT_SIGNED")]
    NotSigned,

    [PgName("SIGNED")]
    Signed,

    [PgName("REJECTED")]
    Rejected,
}
