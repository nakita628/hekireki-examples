package langfuse.models

enum class GatewayConnectionStatus(
    val dbName: String,
) {
    ENABLED("enabled"),
    DISABLED("disabled"),
    ERROR("error"),
}
