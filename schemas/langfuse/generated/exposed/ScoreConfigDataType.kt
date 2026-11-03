package langfuse.models

enum class ScoreConfigDataType(
    val dbName: String,
) {
    CATEGORICAL("CATEGORICAL"),
    NUMERIC("NUMERIC"),
    BOOLEAN("BOOLEAN"),
    TEXT("TEXT"),
}
