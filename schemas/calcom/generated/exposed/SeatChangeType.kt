package calcom.models

enum class SeatChangeType(
    val dbName: String,
) {
    ADDITION("ADDITION"),
    REMOVAL("REMOVAL"),
}
