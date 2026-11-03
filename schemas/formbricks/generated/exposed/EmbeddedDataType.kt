package formbricks.models

enum class EmbeddedDataType(
    val dbName: String,
) {
    STRING("string"),
    NUMBER("number"),
    BOOLEAN("boolean"),
    DATE("date"),
}
