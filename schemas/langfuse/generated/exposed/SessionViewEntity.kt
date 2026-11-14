package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SessionViewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionViewEntity>(SessionViewTable)

    var isPinnedByDefaultForProject by SessionViewTable.isPinnedByDefaultForProject
    var projectId by SessionViewTable.projectId
    var project by ProjectEntity referencedOn SessionViewTable.projectId
    var name by SessionViewTable.name
    var filters by SessionViewTable.filters
    var viewMode by SessionViewTable.viewMode
    var unmatchedTraces by SessionViewTable.unmatchedTraces
    var createdBy by SessionViewTable.createdBy
    var createdByUser by UserEntity optionalReferencedOn SessionViewTable.createdBy
    var createdAt by SessionViewTable.createdAt
    var updatedAt by SessionViewTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SessionViewTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
