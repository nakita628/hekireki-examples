package inboxzero.models

enum class SystemType(
    val dbName: String,
) {
    TO_REPLY("TO_REPLY"),
    FYI("FYI"),
    AWAITING_REPLY("AWAITING_REPLY"),
    ACTIONED("ACTIONED"),
    COLD_EMAIL("COLD_EMAIL"),
    NEWSLETTER("NEWSLETTER"),
    MARKETING("MARKETING"),
    CALENDAR("CALENDAR"),
    RECEIPT("RECEIPT"),
    NOTIFICATION("NOTIFICATION"),
    OTP("OTP"),
}
