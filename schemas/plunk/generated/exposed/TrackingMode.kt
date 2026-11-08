package plunk.models

enum class TrackingMode(
    val dbName: String,
) {
    ENABLED("ENABLED"),
    DISABLED("DISABLED"),
    MARKETING_ONLY("MARKETING_ONLY"),
}
