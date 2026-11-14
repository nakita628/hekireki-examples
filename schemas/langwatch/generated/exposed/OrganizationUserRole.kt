package langwatch.models

enum class OrganizationUserRole(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MEMBER("MEMBER"),
    EXTERNAL("EXTERNAL"),
    DEVELOPER("DEVELOPER"),
}
