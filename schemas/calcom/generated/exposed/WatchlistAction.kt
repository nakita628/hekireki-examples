package calcom.models

enum class WatchlistAction(
    val dbName: String,
) {
    REPORT("REPORT"),
    BLOCK("BLOCK"),
    ALERT("ALERT"),
}
