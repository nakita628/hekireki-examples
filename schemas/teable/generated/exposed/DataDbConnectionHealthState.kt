package teable.models

enum class DataDbConnectionHealthState(
    val dbName: String,
) {
    HEALTHY("healthy"),
    READ_ONLY("read_only"),
    UNREACHABLE("unreachable"),
    DEGRADED("degraded"),
}
