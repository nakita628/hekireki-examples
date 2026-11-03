package documenso.models

enum class IdentityProvider(
    val dbName: String,
) {
    DOCUMENSO("DOCUMENSO"),
    GOOGLE("GOOGLE"),
    OIDC("OIDC"),
}
