package formbricks.models

enum class RetentionTargetType(
    val dbName: String,
) {
    SURVEY("survey"),
    USER("user"),
}
