package rallly.models

enum class LicenseType(
    val dbName: String,
) {
    PLUS("PLUS"),
    ORGANIZATION("ORGANIZATION"),
    ENTERPRISE("ENTERPRISE"),
}
