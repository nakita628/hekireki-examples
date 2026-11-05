#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum LogicalOperator
{
    [PgName("AND")]
    And,

    [PgName("OR")]
    Or,
}
