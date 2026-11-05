#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum CategoryFilterType
{
    [PgName("INCLUDE")]
    Include,

    [PgName("EXCLUDE")]
    Exclude,
}
