package calcom.models

enum class IdentityProvider(
    val dbName: String,
) {
    CAL("CAL"),
    GOOGLE("GOOGLE"),
    SAML("SAML"),
    AZUREAD("AZUREAD"),
}
