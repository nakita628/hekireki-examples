#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum Provider
{
    [PgName("ANONYMOUS")]
    Anonymous,

    [PgName("GOOGLE")]
    Google,

    [PgName("INTERNET_IDENTITY")]
    InternetIdentity,

    [PgName("OIDC")]
    Oidc,
}
