#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum CredentialType
{
    [PgName("OAUTH")]
    Oauth,
}
