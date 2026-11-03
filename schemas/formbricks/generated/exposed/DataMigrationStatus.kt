package formbricks.models

enum class DataMigrationStatus(
    val dbName: String,
) {
    PENDING("pending"),
    APPLIED("applied"),
    FAILED("failed"),
}
