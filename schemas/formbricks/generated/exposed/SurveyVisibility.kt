package formbricks.models

enum class SurveyVisibility(
    val dbName: String,
) {
    RESTRICTED("restricted"),
    WORKSPACE("workspace"),
}
