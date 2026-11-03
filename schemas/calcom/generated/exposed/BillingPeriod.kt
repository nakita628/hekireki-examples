package calcom.models

enum class BillingPeriod(
    val dbName: String,
) {
    MONTHLY("MONTHLY"),
    ANNUALLY("ANNUALLY"),
}
