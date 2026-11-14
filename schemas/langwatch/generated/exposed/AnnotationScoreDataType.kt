package langwatch.models

enum class AnnotationScoreDataType(
    val dbName: String,
) {
    CATEGORICAL("CATEGORICAL"),
    BOOLEAN("BOOLEAN"),
    LIKERT("LIKERT"),
    OPTION("OPTION"),
    CHECKBOX("CHECKBOX"),
}
