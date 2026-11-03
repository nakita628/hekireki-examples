#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum LicenseType
{
    [PgName("PLUS")]
    Plus,

    [PgName("ORGANIZATION")]
    Organization,

    [PgName("ENTERPRISE")]
    Enterprise,
}
