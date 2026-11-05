package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class EventMetaEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, EventMetaEntity>(EventMetaTable)

    var name by EventMetaTable.name
    var conversion by EventMetaTable.conversion
    var color by EventMetaTable.color
    var icon by EventMetaTable.icon
    var projectId by EventMetaTable.projectId
    var project by ProjectEntity referencedOn EventMetaTable.projectId
    var createdAt by EventMetaTable.createdAt
    var updatedAt by EventMetaTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EventMetaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
