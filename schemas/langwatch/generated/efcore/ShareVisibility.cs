#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ShareVisibility
{
    [PgName("PUBLIC")]
    Public,

    [PgName("ORGANIZATION")]
    Organization,

    [PgName("PROJECT")]
    Project,
}
