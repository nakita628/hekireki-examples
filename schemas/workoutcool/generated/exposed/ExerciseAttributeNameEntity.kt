package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExerciseAttributeNameEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExerciseAttributeNameEntity>(ExerciseAttributeNameTable)

    var name by ExerciseAttributeNameTable.name
    var createdAt by ExerciseAttributeNameTable.createdAt
    var updatedAt by ExerciseAttributeNameTable.updatedAt
    val values by ExerciseAttributeValueEntity referrersOn ExerciseAttributeValueTable.attributeNameId
    val attributes by ExerciseAttributeEntity referrersOn ExerciseAttributeTable.attributeNameId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExerciseAttributeNameTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
