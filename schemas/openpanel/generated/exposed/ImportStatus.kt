package openpanel.models

enum class ImportStatus(
    val dbName: String,
) {
    PENDING("pending"),
    PROCESSING("processing"),
    COMPLETED("completed"),
    FAILED("failed"),
}
