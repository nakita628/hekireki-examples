package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MonitorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MonitorEntity>(MonitorTable)

    var projectId by MonitorTable.projectId
    var experimentId by MonitorTable.experimentId
    var evaluatorId by MonitorTable.evaluatorId
    var checkType by MonitorTable.checkType
    var name by MonitorTable.name
    var slug by MonitorTable.slug
    var executionMode by MonitorTable.executionMode
    var enabled by MonitorTable.enabled
    var preconditions by MonitorTable.preconditions
    var parameters by MonitorTable.parameters
    var mappings by MonitorTable.mappings
    var sample by MonitorTable.sample
    var level by MonitorTable.level
    var threadIdleTimeout by MonitorTable.threadIdleTimeout
    var createdAt by MonitorTable.createdAt
    var updatedAt by MonitorTable.updatedAt
    var autoPausedAt by MonitorTable.autoPausedAt
    var autoPausedReason by MonitorTable.autoPausedReason
    var evaluatorLabel by MonitorTable.evaluatorLabel
    var createdById by MonitorTable.createdById
    var updatedById by MonitorTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MonitorTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
