package langwatch.models

enum class GatewayCacheRuleMode(
    val dbName: String,
) {
    RESPECT("RESPECT"),
    FORCE("FORCE"),
    DISABLE("DISABLE"),
}
