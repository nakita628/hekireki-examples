#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum OidcSigningKeyStatus
{
    [PgName("STAGED")]
    Staged,

    [PgName("ACTIVE")]
    Active,

    [PgName("RETIRED")]
    Retired,
}
