package langwatch.models

enum class GatewayBudgetLedgerStatus(
    val dbName: String,
) {
    SUCCESS("SUCCESS"),
    PROVIDER_ERROR("PROVIDER_ERROR"),
    BLOCKED_BY_GUARDRAIL("BLOCKED_BY_GUARDRAIL"),
    CANCELLED("CANCELLED"),
}
