package plunk.models

enum class CardVerificationStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    VERIFIED("VERIFIED"),
    REQUIRES_ACTION("REQUIRES_ACTION"),
    FAILED("FAILED"),
}
