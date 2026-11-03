package calcom.models

enum class RoleType(
    val dbName: String,
) {
    SYSTEM("SYSTEM"),
    CUSTOM("CUSTOM"),
}
