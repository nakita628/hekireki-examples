package inboxzero.models

enum class MobilePushTokenType(
    val dbName: String,
) {
    EXPO("expo"),
    APNS("apns"),
    FCM("fcm"),
}
