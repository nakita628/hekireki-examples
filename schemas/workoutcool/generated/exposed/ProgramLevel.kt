package workoutcool.models

enum class ProgramLevel(
    val dbName: String,
) {
    BEGINNER("BEGINNER"),
    INTERMEDIATE("INTERMEDIATE"),
    ADVANCED("ADVANCED"),
    EXPERT("EXPERT"),
}
