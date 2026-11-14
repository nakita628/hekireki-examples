package langfuse.models

enum class SessionUnmatchedTraces(
    val dbName: String,
) {
    SHOW("show"),
    HIDE("hide"),
}
