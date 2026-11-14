package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExperimentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExperimentEntity>(ExperimentTable)

    var name by ExperimentTable.name
    var type by ExperimentTable.type
    var slug by ExperimentTable.slug
    var projectId by ExperimentTable.projectId
    var workflowId by ExperimentTable.workflowId
    var createdAt by ExperimentTable.createdAt
    var updatedAt by ExperimentTable.updatedAt
    var archivedAt by ExperimentTable.archivedAt
    var workbenchState by ExperimentTable.workbenchState
    var workbenchVersion by ExperimentTable.workbenchVersion

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExperimentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
