package langfuse.models

enum class MonitorView(
    val dbName: String,
) {
    OBSERVATIONS("OBSERVATIONS"),
    SCORES_NUMERIC("SCORES_NUMERIC"),
    SCORES_CATEGORICAL("SCORES_CATEGORICAL"),
    SCORES_BOOLEAN("SCORES_BOOLEAN"),
}
