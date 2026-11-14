package langwatch.models

enum class GatewayBudgetBreachAction(
    val dbName: String,
) {
    BLOCK("BLOCK"),
    WARN("WARN"),
}
