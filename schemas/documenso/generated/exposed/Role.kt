package documenso.models

enum class Role(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    USER("USER"),
}
