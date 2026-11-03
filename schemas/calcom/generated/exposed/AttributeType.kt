package calcom.models

enum class AttributeType(
    val dbName: String,
) {
    TEXT("TEXT"),
    NUMBER("NUMBER"),
    SINGLE_SELECT("SINGLE_SELECT"),
    MULTI_SELECT("MULTI_SELECT"),
}
