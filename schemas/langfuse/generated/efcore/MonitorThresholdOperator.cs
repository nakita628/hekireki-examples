#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum MonitorThresholdOperator
{
    [PgName("GT")]
    Gt,

    [PgName("GTE")]
    Gte,

    [PgName("LT")]
    Lt,

    [PgName("LTE")]
    Lte,

    [PgName("EQ")]
    Eq,

    [PgName("NEQ")]
    Neq,
}
