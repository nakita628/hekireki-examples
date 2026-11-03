package calcom.models

enum class OAuthClientStatus(
    val dbName: String,
) {
    PENDING("pending"),
    APPROVED("approved"),
    REJECTED("rejected"),
}
