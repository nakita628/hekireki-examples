#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum IdentityProvider
{
    [PgName("DOCUMENSO")]
    Documenso,

    [PgName("GOOGLE")]
    Google,

    [PgName("OIDC")]
    Oidc,
}
