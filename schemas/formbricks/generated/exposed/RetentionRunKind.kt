package formbricks.models

enum class RetentionRunKind(
    val dbName: String,
) {
    SWEEP("sweep"),
    PURGE("purge"),
}
