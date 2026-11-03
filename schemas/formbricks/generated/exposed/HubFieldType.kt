package formbricks.models

enum class HubFieldType(
    val dbName: String,
) {
    TEXT("text"),
    CATEGORICAL("categorical"),
    NPS("nps"),
    CSAT("csat"),
    CES("ces"),
    RATING("rating"),
    NUMBER("number"),
    BOOLEAN("boolean"),
    DATE("date"),
}
