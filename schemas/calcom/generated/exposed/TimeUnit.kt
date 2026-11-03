package calcom.models

enum class TimeUnit(
    val dbName: String,
) {
    DAY("day"),
    HOUR("hour"),
    MINUTE("minute"),
}
