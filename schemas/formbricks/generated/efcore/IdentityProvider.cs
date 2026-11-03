#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum IdentityProvider
{
    [PgName("email")]
    Email,

    [PgName("github")]
    Github,

    [PgName("google")]
    Google,

    [PgName("azuread")]
    Azuread,

    [PgName("openid")]
    Openid,

    [PgName("saml")]
    Saml,
}
