package langwatch.models

enum class ConnectedInvoiceKind(
    val dbName: String,
) {
    ANNUAL("annual"),
    SEAT_CHANGE("seat_change"),
    USAGE("usage"),
}
