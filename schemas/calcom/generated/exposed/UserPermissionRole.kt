package calcom.models

enum class UserPermissionRole(
    val dbName: String,
) {
    USER("USER"),
    ADMIN("ADMIN"),
}
