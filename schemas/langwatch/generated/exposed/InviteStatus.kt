package langwatch.models

enum class InviteStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    ACCEPTED("ACCEPTED"),
    WAITING_APPROVAL("WAITING_APPROVAL"),
    PAYMENT_PENDING("PAYMENT_PENDING"),
    REVOKED("REVOKED"),
}
