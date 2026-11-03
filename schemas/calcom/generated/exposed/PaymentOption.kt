package calcom.models

enum class PaymentOption(
    val dbName: String,
) {
    ON_BOOKING("ON_BOOKING"),
    HOLD("HOLD"),
}
