package documenso.models

enum class DocumentVisibility(
    val dbName: String,
) {
    EVERYONE("EVERYONE"),
    MANAGER_AND_ABOVE("MANAGER_AND_ABOVE"),
    ADMIN("ADMIN"),
}
