#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayBudgetLedgerStatus
{
    [PgName("SUCCESS")]
    Success,

    [PgName("PROVIDER_ERROR")]
    ProviderError,

    [PgName("BLOCKED_BY_GUARDRAIL")]
    BlockedByGuardrail,

    [PgName("CANCELLED")]
    Cancelled,
}
