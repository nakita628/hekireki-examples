package inboxzero.models

enum class ReferralStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    COMPLETED("COMPLETED"),
}
