package formbricks.models

enum class RetentionExemptionSource(
    val dbName: String,
) {
    MANUAL("manual"),
    RESTORE("restore"),
}
