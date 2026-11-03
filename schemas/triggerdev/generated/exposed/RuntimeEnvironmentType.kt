package triggerdev.models

enum class RuntimeEnvironmentType(
    val dbName: String,
) {
    PRODUCTION("PRODUCTION"),
    STAGING("STAGING"),
    DEVELOPMENT("DEVELOPMENT"),
    PREVIEW("PREVIEW"),
}
