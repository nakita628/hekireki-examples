package formbricks.models

enum class IdentityProvider(
    val dbName: String,
) {
    EMAIL("email"),
    GITHUB("github"),
    GOOGLE("google"),
    AZUREAD("azuread"),
    OPENID("openid"),
    SAML("saml"),
}
