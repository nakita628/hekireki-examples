package litellm.models

enum class AccountingDeliveryState(
    val dbName: String,
) {
    PENDING("pending"),
    APPLIED("applied"),
    PARKED("parked"),
}
