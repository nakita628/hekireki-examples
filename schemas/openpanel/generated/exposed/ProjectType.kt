package openpanel.models

enum class ProjectType(
    val dbName: String,
) {
    WEBSITE("website"),
    APP("app"),
    BACKEND("backend"),
}
