package plunk.models

enum class CampaignStatus(
    val dbName: String,
) {
    DRAFT("DRAFT"),
    SCHEDULED("SCHEDULED"),
    SENDING("SENDING"),
    SENT("SENT"),
    CANCELLED("CANCELLED"),
}
