package linkwarden.models

enum class AppMigrationStatus(
    val dbName: String,
) {
    APPLIED("APPLIED"),
    PENDING("PENDING"),
    FAILED("FAILED"),
}
