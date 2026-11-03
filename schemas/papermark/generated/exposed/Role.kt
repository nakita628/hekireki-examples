package papermark.models

enum class Role(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MANAGER("MANAGER"),
    MEMBER("MEMBER"),
    DATAROOM_MEMBER("DATAROOM_MEMBER"),
}
