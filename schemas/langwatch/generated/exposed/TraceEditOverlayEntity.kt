package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TraceEditOverlayEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TraceEditOverlayEntity>(TraceEditOverlayTable)

    var projectId by TraceEditOverlayTable.projectId
    var traceId by TraceEditOverlayTable.traceId
    var patch by TraceEditOverlayTable.patch
    var createdById by TraceEditOverlayTable.createdById
    var updatedById by TraceEditOverlayTable.updatedById
    var createdAt by TraceEditOverlayTable.createdAt
    var updatedAt by TraceEditOverlayTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TraceEditOverlayTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
