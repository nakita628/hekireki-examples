package triggerdev.models

enum class OrganizationAccessTokenType(
    val dbName: String,
) {
    USER("USER"),
    SYSTEM("SYSTEM"),
}
