package calcom.models

enum class PhoneNumberSubscriptionStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    PAST_DUE("PAST_DUE"),
    CANCELLED("CANCELLED"),
    INCOMPLETE("INCOMPLETE"),
    INCOMPLETE_EXPIRED("INCOMPLETE_EXPIRED"),
    TRIALING("TRIALING"),
    UNPAID("UNPAID"),
}
