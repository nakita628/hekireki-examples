package calcom.models

enum class CreditType(
    val dbName: String,
) {
    MONTHLY("MONTHLY"),
    ADDITIONAL("ADDITIONAL"),
}
