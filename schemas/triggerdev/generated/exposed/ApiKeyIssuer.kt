package triggerdev.models

enum class ApiKeyIssuer(
    val dbName: String,
) {
    USER("USER"),
    SYSTEM("SYSTEM"),
}
