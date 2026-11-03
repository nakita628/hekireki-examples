package postiz.models

enum class Role(
    val dbName: String,
) {
    SUPERADMIN("SUPERADMIN"),
    ADMIN("ADMIN"),
    USER("USER"),
}
