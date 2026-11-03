package calcom.models

enum class EventTypeCustomInputType(
    val dbName: String,
) {
    TEXT("text"),
    TEXTLONG("textLong"),
    NUMBER("number"),
    BOOL("bool"),
    RADIO("radio"),
    PHONE("phone"),
}
