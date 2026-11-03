package postiz.models

enum class OrderStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    ACCEPTED("ACCEPTED"),
    CANCELED("CANCELED"),
    COMPLETED("COMPLETED"),
}
