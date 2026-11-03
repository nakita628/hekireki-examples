package calcom.models

enum class ProrationStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    INVOICE_CREATED("INVOICE_CREATED"),
    CHARGED("CHARGED"),
    FAILED("FAILED"),
    CANCELLED("CANCELLED"),
}
