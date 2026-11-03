package triggerdev.models

enum class OidcSigningKeyStatus(
    val dbName: String,
) {
    STAGED("STAGED"),
    ACTIVE("ACTIVE"),
    RETIRED("RETIRED"),
}
