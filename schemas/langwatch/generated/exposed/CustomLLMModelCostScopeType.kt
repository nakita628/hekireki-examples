package langwatch.models

enum class CustomLLMModelCostScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    TEAM("TEAM"),
    PROJECT("PROJECT"),
}
