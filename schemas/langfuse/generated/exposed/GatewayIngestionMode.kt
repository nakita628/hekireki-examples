package langfuse.models

enum class GatewayIngestionMode(
    val dbName: String,
) {
    NONE("none"),
    USAGE("usage"),
    FULL("full"),
}
