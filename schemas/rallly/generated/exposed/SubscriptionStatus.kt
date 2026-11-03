package rallly.models

enum class SubscriptionStatus(
    val dbName: String,
) {
    INCOMPLETE("incomplete"),
    INCOMPLETE_EXPIRED("incomplete_expired"),
    ACTIVE("active"),
    PAUSED("paused"),
    TRIALING("trialing"),
    PAST_DUE("past_due"),
    CANCELED("canceled"),
    UNPAID("unpaid"),
}
