package langwatch.models

enum class FeatureFlagExperimentSubjectType(
    val dbName: String,
) {
    USER("USER"),
    ORGANIZATION("ORGANIZATION"),
    PROJECT("PROJECT"),
}
