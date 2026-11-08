package workoutcool.models

enum class ProgramVisibility(
    val dbName: String,
) {
    DRAFT("DRAFT"),
    PUBLISHED("PUBLISHED"),
    ARCHIVED("ARCHIVED"),
}
