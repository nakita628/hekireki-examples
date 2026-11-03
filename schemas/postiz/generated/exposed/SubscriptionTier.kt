package postiz.models

enum class SubscriptionTier(
    val dbName: String,
) {
    STANDARD("STANDARD"),
    PRO("PRO"),
    TEAM("TEAM"),
    ULTIMATE("ULTIMATE"),
}
