package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ObservationMediaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ObservationMediaEntity>(ObservationMediaTable)

    var projectId by ObservationMediaTable.projectId
    var createdAt by ObservationMediaTable.createdAt
    var updatedAt by ObservationMediaTable.updatedAt
    var mediaId by ObservationMediaTable.mediaId
    var traceId by ObservationMediaTable.traceId
    var observationId by ObservationMediaTable.observationId
    var field by ObservationMediaTable.field
    var origin by ObservationMediaTable.origin

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ObservationMediaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
