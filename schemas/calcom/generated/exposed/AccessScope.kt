package calcom.models

enum class AccessScope(
    val dbName: String,
) {
    READ_BOOKING("READ_BOOKING"),
    READ_PROFILE("READ_PROFILE"),
}
