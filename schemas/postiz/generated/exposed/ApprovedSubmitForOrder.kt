package postiz.models

enum class ApprovedSubmitForOrder(
    val dbName: String,
) {
    NO("NO"),
    WAITING_CONFIRMATION("WAITING_CONFIRMATION"),
    YES("YES"),
}
