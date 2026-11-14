package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TriggerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TriggerEntity>(TriggerTable)

    var name by TriggerTable.name
    var projectId by TriggerTable.projectId
    var action by TriggerTable.action
    var triggerKind by TriggerTable.triggerKind
    var actionParams by TriggerTable.actionParams
    var filters by TriggerTable.filters
    var filterQuery by TriggerTable.filterQuery
    var lastRunAt by TriggerTable.lastRunAt
    var createdAt by TriggerTable.createdAt
    var updatedAt by TriggerTable.updatedAt
    var active by TriggerTable.active
    var pausedReason by TriggerTable.pausedReason
    var pausedAt by TriggerTable.pausedAt
    var message by TriggerTable.message
    var deleted by TriggerTable.deleted
    var alertType by TriggerTable.alertType
    var slackTemplateType by TriggerTable.slackTemplateType
    var slackTemplate by TriggerTable.slackTemplate
    var emailSubjectTemplate by TriggerTable.emailSubjectTemplate
    var emailBodyTemplate by TriggerTable.emailBodyTemplate
    var notificationCadence by TriggerTable.notificationCadence
    var traceDebounceMs by TriggerTable.traceDebounceMs
    var customGraphId by TriggerTable.customGraphId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TriggerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
