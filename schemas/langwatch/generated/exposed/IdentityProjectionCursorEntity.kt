package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IdentityProjectionCursorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IdentityProjectionCursorEntity>(IdentityProjectionCursorTable)

    var lastEventId by IdentityProjectionCursorTable.lastEventId
    var acceptedAt by IdentityProjectionCursorTable.acceptedAt
    var occurredAt by IdentityProjectionCursorTable.occurredAt
    var projectionVersion by IdentityProjectionCursorTable.projectionVersion
    var createdAt by IdentityProjectionCursorTable.createdAt
    var updatedAt by IdentityProjectionCursorTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IdentityProjectionCursorTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
