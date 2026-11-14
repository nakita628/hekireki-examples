#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayGuardrailFailureMode
{
    [PgName("FAIL_OPEN")]
    FailOpen,

    [PgName("FAIL_CLOSED")]
    FailClosed,
}
