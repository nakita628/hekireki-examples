package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProgramSuggestedSetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProgramSuggestedSetEntity>(ProgramSuggestedSetTable)

    var programSessionExerciseId by ProgramSuggestedSetTable.programSessionExerciseId
    var setIndex by ProgramSuggestedSetTable.setIndex
    var types by ProgramSuggestedSetTable.types
    var valuesInt by ProgramSuggestedSetTable.valuesInt
    var valuesSec by ProgramSuggestedSetTable.valuesSec
    var units by ProgramSuggestedSetTable.units
    var programSessionExercise by ProgramSessionExerciseEntity referencedOn ProgramSuggestedSetTable.programSessionExerciseId
}
