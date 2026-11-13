#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ApiKeyIssuer
{
    [PgName("USER")]
    User,

    [PgName("SYSTEM")]
    System,
}
