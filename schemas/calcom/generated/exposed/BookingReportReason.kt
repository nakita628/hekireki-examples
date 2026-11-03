package calcom.models

enum class BookingReportReason(
    val dbName: String,
) {
    SPAM("SPAM"),
    DONT_KNOW_PERSON("dont_know_person"),
    OTHER("OTHER"),
}
