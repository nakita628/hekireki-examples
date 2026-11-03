package langfuse.models

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

    var createdAt by MonitorTable.createdAt
    var updatedAt by MonitorTable.updatedAt
    var createdBy by MonitorTable.createdBy
    var createdByUser by UserEntity optionalReferencedOn MonitorTable.createdBy
    var updatedBy by MonitorTable.updatedBy
    var updatedByUser by UserEntity optionalReferencedOn MonitorTable.updatedBy
    var projectId by MonitorTable.projectId
    var project by ProjectEntity referencedOn MonitorTable.projectId
    var view by MonitorTable.view
    var filters by MonitorTable.filters
    var metric by MonitorTable.metric
    var windowMs by MonitorTable.windowMs
    var cadenceMs by MonitorTable.cadenceMs
    var thresholdOperator by MonitorTable.thresholdOperator
    var alertThreshold by MonitorTable.alertThreshold
    var warningThreshold by MonitorTable.warningThreshold
    var severity by MonitorTable.severity
    var severityChangedAt by MonitorTable.severityChangedAt
    var noData by MonitorTable.noData
    var renotify by MonitorTable.renotify
    var status by MonitorTable.status
    var schedulerBatchId by MonitorTable.schedulerBatchId
    var nextRunAt by MonitorTable.nextRunAt
    var lastPublishedAt by MonitorTable.lastPublishedAt
    var lastClaimedAt by MonitorTable.lastClaimedAt
    var lastCompletedAt by MonitorTable.lastCompletedAt
    var name by MonitorTable.name
    var tags by MonitorTable.tags
    var triggerIds by MonitorTable.triggerIds
    var alertedAt by MonitorTable.alertedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MonitorTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
