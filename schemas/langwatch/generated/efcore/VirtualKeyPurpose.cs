#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum VirtualKeyPurpose
{
    [PgName("USER")]
    User,

    [PgName("LANGY")]
    Langy,

    [PgName("CONNECT")]
    Connect,
}
