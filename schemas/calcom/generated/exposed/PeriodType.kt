package calcom.models

enum class PeriodType(
    val dbName: String,
) {
    UNLIMITED("unlimited"),
    ROLLING("rolling"),
    ROLLING_WINDOW("rolling_window"),
    RANGE("range"),
}
