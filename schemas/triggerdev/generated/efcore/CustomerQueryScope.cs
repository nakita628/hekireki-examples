#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum CustomerQueryScope
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("PROJECT")]
    Project,

    [PgName("ENVIRONMENT")]
    Environment,
}
