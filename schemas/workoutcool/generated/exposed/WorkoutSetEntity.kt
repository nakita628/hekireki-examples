package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkoutSetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkoutSetEntity>(WorkoutSetTable)

    var workoutSessionExerciseId by WorkoutSetTable.workoutSessionExerciseId
    var setIndex by WorkoutSetTable.setIndex
    var type by WorkoutSetTable.type
    var types by WorkoutSetTable.types
    var valuesInt by WorkoutSetTable.valuesInt
    var valuesSec by WorkoutSetTable.valuesSec
    var units by WorkoutSetTable.units
    var completed by WorkoutSetTable.completed
    var workoutSessionExercise by WorkoutSessionExerciseEntity referencedOn WorkoutSetTable.workoutSessionExerciseId
}
