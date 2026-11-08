package workoutcool.models

enum class ExerciseAttributeNameEnum(
    val dbName: String,
) {
    TYPE("TYPE"),
    PRIMARY_MUSCLE("PRIMARY_MUSCLE"),
    SECONDARY_MUSCLE("SECONDARY_MUSCLE"),
    EQUIPMENT("EQUIPMENT"),
    MECHANICS_TYPE("MECHANICS_TYPE"),
}
