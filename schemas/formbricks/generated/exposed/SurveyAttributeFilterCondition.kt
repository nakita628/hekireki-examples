package formbricks.models

enum class SurveyAttributeFilterCondition(
    val dbName: String,
) {
    EQUALS("equals"),
    NOT_EQUALS("notEquals"),
}
