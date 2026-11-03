package triggerdev.models

enum class OrgMemberRole(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MEMBER("MEMBER"),
}
