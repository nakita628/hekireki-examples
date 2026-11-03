package postiz.models

enum class State(
    val dbName: String,
) {
    QUEUE("QUEUE"),
    PUBLISHED("PUBLISHED"),
    ERROR("ERROR"),
    DRAFT("DRAFT"),
}
