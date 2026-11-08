package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExerciseAttributeValueEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExerciseAttributeValueEntity>(ExerciseAttributeValueTable)

    var attributeNameId by ExerciseAttributeValueTable.attributeNameId
    var value by ExerciseAttributeValueTable.value
    var createdAt by ExerciseAttributeValueTable.createdAt
    var updatedAt by ExerciseAttributeValueTable.updatedAt
    var attributeName by ExerciseAttributeNameEntity referencedOn ExerciseAttributeValueTable.attributeNameId
    val attributes by ExerciseAttributeEntity referrersOn ExerciseAttributeTable.attributeValueId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExerciseAttributeValueTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
