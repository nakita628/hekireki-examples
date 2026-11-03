package calcom.models

enum class RRTimestampBasis(
    val dbName: String,
) {
    CREATED_AT("CREATED_AT"),
    START_TIME("START_TIME"),
}
