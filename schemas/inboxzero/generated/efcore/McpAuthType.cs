#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum McpAuthType
{
    [PgName("OAUTH")]
    Oauth,

    [PgName("API_TOKEN")]
    ApiToken,

    [PgName("NONE")]
    None,
}
