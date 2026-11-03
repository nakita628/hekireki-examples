package documenso.models

enum class SubscriptionStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    PAST_DUE("PAST_DUE"),
    INACTIVE("INACTIVE"),
}
