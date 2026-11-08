package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExerciseAttributeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExerciseAttributeEntity>(ExerciseAttributeTable)

    var exerciseId by ExerciseAttributeTable.exerciseId
    var attributeNameId by ExerciseAttributeTable.attributeNameId
    var attributeValueId by ExerciseAttributeTable.attributeValueId
    var createdAt by ExerciseAttributeTable.createdAt
    var updatedAt by ExerciseAttributeTable.updatedAt
    var exercise by ExerciseEntity referencedOn ExerciseAttributeTable.exerciseId
    var attributeName by ExerciseAttributeNameEntity referencedOn ExerciseAttributeTable.attributeNameId
    var attributeValue by ExerciseAttributeValueEntity referencedOn ExerciseAttributeTable.attributeValueId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExerciseAttributeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
