package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class SessionReplaySavedEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, SessionReplaySavedEntity>(SessionReplaySavedTable)

    var name by SessionReplaySavedTable.name
    var websiteId by SessionReplaySavedTable.websiteId
    var visitId by SessionReplaySavedTable.visitId
    var createdAt by SessionReplaySavedTable.createdAt
    var updatedAt by SessionReplaySavedTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SessionReplaySavedTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
