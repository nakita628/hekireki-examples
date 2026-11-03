#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum AccessType
{
    [PgName("MCP")]
    Mcp,

    [PgName("PRIVATE")]
    Private,

    [PgName("PUBLIC")]
    Public,
}
