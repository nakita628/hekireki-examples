package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExerciseAttributeValueTable : IdTable<String>("exercise_attribute_values") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val attributeNameId =
        reference(
            "attributeNameId",
            ExerciseAttributeNameTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"exercise_attribute_values_attributeNameId_fkey\"",
        )
    val value = pgEnum("value", "\"ExerciseAttributeValueEnum\"", ExerciseAttributeValueEnum.entries, ExerciseAttributeValueEnum::dbName)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "exercise_attribute_values_pkey")

    init {
        uniqueIndex("\"exercise_attribute_values_attributeNameId_value_key\"", attributeNameId, value)
    }
}
