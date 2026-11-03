package calcom.models

enum class SMSLockState(
    val dbName: String,
) {
    LOCKED("LOCKED"),
    UNLOCKED("UNLOCKED"),
    REVIEW_NEEDED("REVIEW_NEEDED"),
}
