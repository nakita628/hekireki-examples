package langfuse.models

enum class AuditLogRecordType(
    val dbName: String,
) {
    USER("USER"),
    API_KEY("API_KEY"),
}
