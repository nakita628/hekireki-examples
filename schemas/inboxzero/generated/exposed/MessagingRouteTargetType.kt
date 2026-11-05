package inboxzero.models

enum class MessagingRouteTargetType(
    val dbName: String,
) {
    CHANNEL("CHANNEL"),
    DIRECT_MESSAGE("DIRECT_MESSAGE"),
}
