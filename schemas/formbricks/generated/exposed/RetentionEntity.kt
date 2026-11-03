package formbricks.models

enum class RetentionEntity(
    val dbName: String,
) {
    RESPONSES("responses"),
    SURVEYS("surveys"),
    MEMBERS("members"),
}
