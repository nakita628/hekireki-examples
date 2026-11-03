package calcom.models

enum class BillingMode(
    val dbName: String,
) {
    SEATS("SEATS"),
    ACTIVE_USERS("ACTIVE_USERS"),
}
