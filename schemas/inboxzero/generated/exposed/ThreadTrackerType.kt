package inboxzero.models

enum class ThreadTrackerType(
    val dbName: String,
) {
    AWAITING("AWAITING"),
    NEEDS_REPLY("NEEDS_REPLY"),
    NEEDS_ACTION("NEEDS_ACTION"),
}
