package workoutcool.models

enum class WorkoutSetType(
    val dbName: String,
) {
    TIME("TIME"),
    WEIGHT("WEIGHT"),
    REPS("REPS"),
    BODYWEIGHT("BODYWEIGHT"),
    NA("NA"),
}
