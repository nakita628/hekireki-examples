package langwatch.models

enum class VirtualKeyRoutingMode(
    val dbName: String,
) {
    NONE("NONE"),
    FALLBACK_ALL("FALLBACK_ALL"),
    POLICY("POLICY"),
}
