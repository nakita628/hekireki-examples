package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkoutSessionExerciseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkoutSessionExerciseEntity>(WorkoutSessionExerciseTable)

    var workoutSessionId by WorkoutSessionExerciseTable.workoutSessionId
    var exerciseId by WorkoutSessionExerciseTable.exerciseId
    var order by WorkoutSessionExerciseTable.order
    var workoutSession by WorkoutSessionEntity referencedOn WorkoutSessionExerciseTable.workoutSessionId
    var exercise by ExerciseEntity referencedOn WorkoutSessionExerciseTable.exerciseId
    val sets by WorkoutSetEntity referrersOn WorkoutSetTable.workoutSessionExerciseId
}
