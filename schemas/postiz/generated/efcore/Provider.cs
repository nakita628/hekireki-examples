#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum Provider
{
    [PgName("LOCAL")]
    Local,

    [PgName("GITHUB")]
    Github,

    [PgName("GOOGLE")]
    Google,

    [PgName("APPLE")]
    Apple,

    [PgName("FARCASTER")]
    Farcaster,

    [PgName("WALLET")]
    Wallet,

    [PgName("GENERIC")]
    Generic,
}
