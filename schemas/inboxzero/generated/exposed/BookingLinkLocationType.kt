package inboxzero.models

enum class BookingLinkLocationType(
    val dbName: String,
) {
    IN_PERSON("IN_PERSON"),
    GOOGLE_MEET("GOOGLE_MEET"),
    MICROSOFT_TEAMS("MICROSOFT_TEAMS"),
    PHONE("PHONE"),
    CUSTOM("CUSTOM"),
}
