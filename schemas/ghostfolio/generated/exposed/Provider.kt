package ghostfolio.models

enum class Provider(
    val dbName: String,
) {
    ANONYMOUS("ANONYMOUS"),
    GOOGLE("GOOGLE"),
    INTERNET_IDENTITY("INTERNET_IDENTITY"),
    OIDC("OIDC"),
}
