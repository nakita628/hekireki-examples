package triggerdev.models

enum class AuthenticationMethod(
    val dbName: String,
) {
    GITHUB("GITHUB"),
    MAGIC_LINK("MAGIC_LINK"),
    GOOGLE("GOOGLE"),
    SSO("SSO"),
}
