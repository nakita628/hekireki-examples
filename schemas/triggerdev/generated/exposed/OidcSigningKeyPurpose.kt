package triggerdev.models

enum class OidcSigningKeyPurpose(
    val dbName: String,
) {
    EXTERNAL_OIDC("EXTERNAL_OIDC"),
    INTERNAL_RUN("INTERNAL_RUN"),
}
