package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class TraceSessionEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<TraceSessionEntity>(TraceSessionTable)

    val id1 by TraceSessionTable.id1
    var createdAt by TraceSessionTable.createdAt
    var updatedAt by TraceSessionTable.updatedAt
    val projectId by TraceSessionTable.projectId
    val project by ProjectEntity referencedOn TraceSessionTable.projectId
    var bookmarked by TraceSessionTable.bookmarked
    var public by TraceSessionTable.public
    var environment by TraceSessionTable.environment

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TraceSessionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
