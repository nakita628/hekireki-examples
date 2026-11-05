package openpanel.models

enum class Interval(
    val dbName: String,
) {
    HOUR("hour"),
    DAY("day"),
    MONTH("month"),
    MINUTE("minute"),
    WEEK("week"),
}
