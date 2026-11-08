package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProgramSessionExerciseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProgramSessionExerciseEntity>(ProgramSessionExerciseTable)

    var sessionId by ProgramSessionExerciseTable.sessionId
    var exerciseId by ProgramSessionExerciseTable.exerciseId
    var order by ProgramSessionExerciseTable.order
    var instructions by ProgramSessionExerciseTable.instructions
    var instructionsEn by ProgramSessionExerciseTable.instructionsEn
    var instructionsEs by ProgramSessionExerciseTable.instructionsEs
    var instructionsPt by ProgramSessionExerciseTable.instructionsPt
    var instructionsRu by ProgramSessionExerciseTable.instructionsRu
    var instructionsZhCn by ProgramSessionExerciseTable.instructionsZhCn
    var session by ProgramSessionEntity referencedOn ProgramSessionExerciseTable.sessionId
    var exercise by ExerciseEntity referencedOn ProgramSessionExerciseTable.exerciseId
    val suggestedSets by ProgramSuggestedSetEntity referrersOn ProgramSuggestedSetTable.programSessionExerciseId
}
