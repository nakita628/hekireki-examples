package langwatch.models

enum class PromptScope(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    PROJECT("PROJECT"),
}
