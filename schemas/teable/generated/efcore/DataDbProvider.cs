#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum DataDbProvider
{
    [PgName("postgres")]
    Postgres,
}
