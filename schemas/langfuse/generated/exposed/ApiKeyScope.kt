package langfuse.models

enum class ApiKeyScope(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    PROJECT("PROJECT"),
}
