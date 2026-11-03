package calcom.models

enum class MembershipRole(
    val dbName: String,
) {
    MEMBER("MEMBER"),
    ADMIN("ADMIN"),
    OWNER("OWNER"),
}
