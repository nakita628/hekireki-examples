#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum DashboardScope
{
    [PgName("PRIVATE")]
    Private,

    [PgName("PROJECT")]
    Project,

    [PgName("ORGANIZATION")]
    Organization,
}
