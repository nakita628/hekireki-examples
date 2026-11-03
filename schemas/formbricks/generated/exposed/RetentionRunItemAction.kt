package formbricks.models

enum class RetentionRunItemAction(
    val dbName: String,
) {
    NOTIFIED("notified"),
    ARCHIVED("archived"),
    DEACTIVATED("deactivated"),
    DELETED("deleted"),
    SKIPPED("skipped"),
}
