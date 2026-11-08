package plunk.models

enum class Role(
    val dbName: String,
) {
    OWNER("OWNER"),
    ADMIN("ADMIN"),
    MEMBER("MEMBER"),
}
