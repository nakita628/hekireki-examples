#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum CreationMethod
{
    [PgName("UNKNOWN")]
    Unknown,

    [PgName("WEB")]
    Web,

    [PgName("MCP")]
    Mcp,

    [PgName("API")]
    Api,

    [PgName("AUTOPOST")]
    Autopost,

    [PgName("CLI")]
    Cli,
}
