package langwatch.models

enum class SubscriptionStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    FAILED("FAILED"),
    ACTIVE("ACTIVE"),
    CANCELLED("CANCELLED"),
}
