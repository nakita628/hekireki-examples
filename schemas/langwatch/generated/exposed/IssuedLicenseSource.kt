package langwatch.models

enum class IssuedLicenseSource(
    val dbName: String,
) {
    BACKOFFICE("BACKOFFICE"),
    PURCHASE("PURCHASE"),
    SCRIPT("SCRIPT"),
    LEGACY_IMPORT("LEGACY_IMPORT"),
}
