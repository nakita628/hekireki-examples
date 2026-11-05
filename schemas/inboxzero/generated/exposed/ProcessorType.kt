package inboxzero.models

enum class ProcessorType(
    val dbName: String,
) {
    LEMON_SQUEEZY("LEMON_SQUEEZY"),
    STRIPE("STRIPE"),
}
