package triggerdev.models

enum class CustomerQueryScope(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    PROJECT("PROJECT"),
    ENVIRONMENT("ENVIRONMENT"),
}
