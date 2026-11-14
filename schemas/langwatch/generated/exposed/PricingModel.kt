package langwatch.models

enum class PricingModel(
    val dbName: String,
) {
    TIERED("TIERED"),
    SEAT_EVENT("SEAT_EVENT"),
}
