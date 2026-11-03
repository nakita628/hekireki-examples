package rallly.models

enum class PollClosedReason(
    val dbName: String,
) {
    AUTO("auto"),
    MANUAL("manual"),
}
