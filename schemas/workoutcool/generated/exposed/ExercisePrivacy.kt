package workoutcool.models

enum class ExercisePrivacy(
    val dbName: String,
) {
    PUBLIC("PUBLIC"),
    PRIVATE("PRIVATE"),
}
