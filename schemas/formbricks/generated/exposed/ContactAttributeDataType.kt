package formbricks.models

enum class ContactAttributeDataType(
    val dbName: String,
) {
    STRING("string"),
    NUMBER("number"),
    DATE("date"),
}
