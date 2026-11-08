package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ProgramSessionExerciseTable : IdTable<String>("program_session_exercises") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val sessionId =
        reference(
            "sessionId",
            ProgramSessionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"program_session_exercises_sessionId_fkey\"",
        )
    val exerciseId =
        reference(
            "exerciseId",
            ExerciseTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"program_session_exercises_exerciseId_fkey\"",
        )
    val order = integer("order")
    val instructions = text("instructions")
    val instructionsEn = text("instructionsEn")
    val instructionsEs = text("instructionsEs")
    val instructionsPt = text("instructionsPt")
    val instructionsRu = text("instructionsRu")
    val instructionsZhCn = text("instructionsZhCn")
    override val primaryKey = PrimaryKey(id, name = "program_session_exercises_pkey")

    init {
        uniqueIndex("\"program_session_exercises_sessionId_order_key\"", sessionId, order)
    }
}
