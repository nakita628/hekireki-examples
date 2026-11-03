package rallly.models

enum class PollStatus(
    val dbName: String,
) {
    OPEN("open"),
    CLOSED("closed"),
    SCHEDULED("scheduled"),
    CANCELED("canceled"),
}
