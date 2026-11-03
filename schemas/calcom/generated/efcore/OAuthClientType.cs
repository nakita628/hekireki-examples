#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum OAuthClientType
{
    [PgName("confidential")]
    Confidential,

    [PgName("public")]
    Public,
}
