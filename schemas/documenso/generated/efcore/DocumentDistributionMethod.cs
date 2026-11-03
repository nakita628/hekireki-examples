#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum DocumentDistributionMethod
{
    [PgName("EMAIL")]
    Email,

    [PgName("NONE")]
    None,
}
