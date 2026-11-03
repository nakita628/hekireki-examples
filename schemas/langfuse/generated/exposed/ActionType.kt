package langfuse.models

enum class ActionType(
    val dbName: String,
) {
    WEBHOOK("WEBHOOK"),
    SLACK("SLACK"),
    GITHUB_DISPATCH("GITHUB_DISPATCH"),
    PAGERDUTY("PAGERDUTY"),
}
