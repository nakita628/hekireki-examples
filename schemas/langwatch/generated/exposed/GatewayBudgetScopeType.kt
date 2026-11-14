package langwatch.models

enum class GatewayBudgetScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    TEAM("TEAM"),
    PROJECT("PROJECT"),
    VIRTUAL_KEY("VIRTUAL_KEY"),
    PRINCIPAL("PRINCIPAL"),
    GROUP("GROUP"),
    ATTRIBUTED_USER("ATTRIBUTED_USER"),
}
