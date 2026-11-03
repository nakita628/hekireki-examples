package rallly.models

enum class SubscriptionInterval(
    val dbName: String,
) {
    MONTH("month"),
    YEAR("year"),
}
