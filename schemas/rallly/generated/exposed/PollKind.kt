package rallly.models

enum class PollKind(
    val dbName: String,
) {
    DATE("date"),
    TIME("time"),
}
