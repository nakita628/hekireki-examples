package triggerdev.models

enum class EnvironmentPauseSource(
    val dbName: String,
) {
    BILLING_LIMIT("BILLING_LIMIT"),
}
