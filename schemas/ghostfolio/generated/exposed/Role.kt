package ghostfolio.models

enum class Role(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    DEMO("DEMO"),
    INACTIVE("INACTIVE"),
    USER("USER"),
}
