package documenso.models

enum class OrganisationMemberRole(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MANAGER("MANAGER"),
    MEMBER("MEMBER"),
}
