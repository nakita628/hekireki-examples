package inboxzero.models

enum class MobilePushPlatform(
    val dbName: String,
) {
    ANDROID("android"),
    IOS("ios"),
}
