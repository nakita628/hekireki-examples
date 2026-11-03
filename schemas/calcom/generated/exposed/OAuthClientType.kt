package calcom.models

enum class OAuthClientType(
    val dbName: String,
) {
    CONFIDENTIAL("confidential"),
    PUBLIC("public"),
}
