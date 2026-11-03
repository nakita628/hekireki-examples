#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum IdentityProvider
{
    [PgName("CAL")]
    Cal,

    [PgName("GOOGLE")]
    Google,

    [PgName("SAML")]
    Saml,

    [PgName("AZUREAD")]
    Azuread,
}
