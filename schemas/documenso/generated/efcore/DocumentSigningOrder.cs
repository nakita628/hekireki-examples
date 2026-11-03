#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum DocumentSigningOrder
{
    [PgName("PARALLEL")]
    Parallel,

    [PgName("SEQUENTIAL")]
    Sequential,
}
