package linkwarden.models

enum class SubscriptionProvider(
    val dbName: String,
) {
    STRIPE("STRIPE"),
    APPLE("APPLE"),
    GOOGLE("GOOGLE"),
}
