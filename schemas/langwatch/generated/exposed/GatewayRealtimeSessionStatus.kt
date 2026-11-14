package langwatch.models

enum class GatewayRealtimeSessionStatus(
    val dbName: String,
) {
    OPEN("OPEN"),
    CLOSED("CLOSED"),
    FAILED("FAILED"),
    EXPIRED("EXPIRED"),
}
