package langwatch.models

enum class GatewayProviderHealthStatus(
    val dbName: String,
) {
    UNKNOWN("UNKNOWN"),
    HEALTHY("HEALTHY"),
    DEGRADED("DEGRADED"),
    CIRCUIT_OPEN("CIRCUIT_OPEN"),
}
