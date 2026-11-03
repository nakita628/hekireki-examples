#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum ApiKeyScope
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("PROJECT")]
    Project,
}
