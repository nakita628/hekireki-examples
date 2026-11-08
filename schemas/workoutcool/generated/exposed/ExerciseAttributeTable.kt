package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExerciseAttributeTable : IdTable<String>("exercise_attributes") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val exerciseId =
        reference(
            "exerciseId",
            ExerciseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"exercise_attributes_exerciseId_fkey\"",
        )
    val attributeNameId =
        reference(
            "attributeNameId",
            ExerciseAttributeNameTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"exercise_attributes_attributeNameId_fkey\"",
        )
    val attributeValueId =
        reference(
            "attributeValueId",
            ExerciseAttributeValueTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"exercise_attributes_attributeValueId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "exercise_attributes_pkey")

    init {
        uniqueIndex("exercise_attributes_exerciseId_attributeNameId_attributeVal_key", exerciseId, attributeNameId, attributeValueId)
    }
}
