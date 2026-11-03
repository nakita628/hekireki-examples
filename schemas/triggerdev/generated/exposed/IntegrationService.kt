package triggerdev.models

enum class IntegrationService(
    val dbName: String,
) {
    SLACK("SLACK"),
    VERCEL("VERCEL"),
}
