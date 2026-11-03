package triggerdev.models

enum class WaitpointStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    COMPLETED("COMPLETED"),
}
