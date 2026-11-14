package langwatch.models

enum class GatewayGuardrailDirection(
    val dbName: String,
) {
    PRE("PRE"),
    POST("POST"),
    STREAM_CHUNK("STREAM_CHUNK"),
}
