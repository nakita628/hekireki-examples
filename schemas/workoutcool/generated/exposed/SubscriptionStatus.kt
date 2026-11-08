package workoutcool.models

enum class SubscriptionStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    TRIAL("TRIAL"),
    CANCELLED("CANCELLED"),
    EXPIRED("EXPIRED"),
    PAUSED("PAUSED"),
}
