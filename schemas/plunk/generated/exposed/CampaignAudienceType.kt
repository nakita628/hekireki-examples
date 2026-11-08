package plunk.models

enum class CampaignAudienceType(
    val dbName: String,
) {
    ALL("ALL"),
    FILTERED("FILTERED"),
    SEGMENT("SEGMENT"),
}
