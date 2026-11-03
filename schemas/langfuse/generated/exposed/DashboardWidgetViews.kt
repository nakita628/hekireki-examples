package langfuse.models

enum class DashboardWidgetViews(
    val dbName: String,
) {
    TRACES("TRACES"),
    OBSERVATIONS("OBSERVATIONS"),
    SCORES_NUMERIC("SCORES_NUMERIC"),
    SCORES_CATEGORICAL("SCORES_CATEGORICAL"),
    SCORES_BOOLEAN("SCORES_BOOLEAN"),
}
