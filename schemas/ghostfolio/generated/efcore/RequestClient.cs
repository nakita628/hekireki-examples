#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum RequestClient
{
    [PgName("API")]
    Api,

    [PgName("MCP")]
    Mcp,

    [PgName("SELF_HOSTING")]
    SelfHosting,

    [PgName("WEB")]
    Web,
}
