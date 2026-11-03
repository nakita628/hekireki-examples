#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum LicenseStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("REVOKED")]
    Revoked,
}
