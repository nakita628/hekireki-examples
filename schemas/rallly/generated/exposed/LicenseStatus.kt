package rallly.models

enum class LicenseStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    REVOKED("REVOKED"),
}
