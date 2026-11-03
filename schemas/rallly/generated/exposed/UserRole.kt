package rallly.models

enum class UserRole(
    val dbName: String,
) {
    ADMIN("admin"),
    USER("user"),
}
