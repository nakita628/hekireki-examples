#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum AuthenticationMethod
{
    [PgName("GITHUB")]
    Github,

    [PgName("MAGIC_LINK")]
    MagicLink,

    [PgName("GOOGLE")]
    Google,

    [PgName("SSO")]
    Sso,
}
