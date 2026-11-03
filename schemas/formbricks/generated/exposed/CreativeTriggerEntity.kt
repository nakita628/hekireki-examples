package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CreativeTriggerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CreativeTriggerEntity>(CreativeTriggerTable)

    var createdAt by CreativeTriggerTable.createdAt
    var updatedAt by CreativeTriggerTable.updatedAt
    var creative by CreativeEntity referencedOn CreativeTriggerTable.creativeId
    var creativeId by CreativeTriggerTable.creativeId
    var actionClass by ActionClassEntity referencedOn CreativeTriggerTable.actionClassId
    var actionClassId by CreativeTriggerTable.actionClassId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CreativeTriggerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
