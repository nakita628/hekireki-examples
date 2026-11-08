package workoutcool.models

enum class PaymentProcessor(
    val dbName: String,
) {
    STRIPE("STRIPE"),
    PAYPAL("PAYPAL"),
    LEMONSQUEEZY("LEMONSQUEEZY"),
    PADDLE("PADDLE"),
    APPLE_PAY("APPLE_PAY"),
    GOOGLE_PAY("GOOGLE_PAY"),
    REVENUECAT("REVENUECAT"),
    NONE("NONE"),
    OTHER("OTHER"),
}
