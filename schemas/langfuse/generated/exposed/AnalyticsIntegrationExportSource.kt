package langfuse.models

enum class AnalyticsIntegrationExportSource(
    val dbName: String,
) {
    TRACES_OBSERVATIONS("TRACES_OBSERVATIONS"),
    TRACES_OBSERVATIONS_EVENTS("TRACES_OBSERVATIONS_EVENTS"),
    EVENTS("EVENTS"),
}
