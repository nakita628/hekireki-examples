package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object WorkoutSessionExerciseTable : IdTable<String>("workout_session_exercises") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val workoutSessionId =
        reference(
            "workoutSessionId",
            WorkoutSessionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workout_session_exercises_workoutSessionId_fkey\"",
        )
    val exerciseId =
        reference(
            "exerciseId",
            ExerciseTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workout_session_exercises_exerciseId_fkey\"",
        )
    val order = integer("order")
    override val primaryKey = PrimaryKey(id, name = "workout_session_exercises_pkey")
}
