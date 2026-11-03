package ghostfolio.models

enum class DataGatheringFrequency(
    val dbName: String,
) {
    DAILY("DAILY"),
    HOURLY("HOURLY"),
}
