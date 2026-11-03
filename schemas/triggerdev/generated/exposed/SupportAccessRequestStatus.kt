package triggerdev.models

enum class SupportAccessRequestStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    APPROVED("APPROVED"),
    CANCELLED("CANCELLED"),
}
