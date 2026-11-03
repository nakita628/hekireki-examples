package papermark.models

enum class CustomFieldType(
    val dbName: String,
) {
    SHORT_TEXT("SHORT_TEXT"),
    LONG_TEXT("LONG_TEXT"),
    NUMBER("NUMBER"),
    PHONE_NUMBER("PHONE_NUMBER"),
    URL("URL"),
    CHECKBOX("CHECKBOX"),
    SELECT("SELECT"),
    MULTI_SELECT("MULTI_SELECT"),
}
