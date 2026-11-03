package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TraceMediaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TraceMediaEntity>(TraceMediaTable)

    var projectId by TraceMediaTable.projectId
    var createdAt by TraceMediaTable.createdAt
    var updatedAt by TraceMediaTable.updatedAt
    var mediaId by TraceMediaTable.mediaId
    var traceId by TraceMediaTable.traceId
    var field by TraceMediaTable.field
    var origin by TraceMediaTable.origin

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TraceMediaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
