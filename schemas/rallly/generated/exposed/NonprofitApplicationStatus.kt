package rallly.models

enum class NonprofitApplicationStatus(
    val dbName: String,
) {
    APPROVED("approved"),
    REJECTED("rejected"),
    FAILED("failed"),
}
