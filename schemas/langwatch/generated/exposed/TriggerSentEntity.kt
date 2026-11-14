package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TriggerSentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TriggerSentEntity>(TriggerSentTable)

    var triggerId by TriggerSentTable.triggerId
    var traceId by TriggerSentTable.traceId
    var customGraphId by TriggerSentTable.customGraphId
    var projectId by TriggerSentTable.projectId
    var resolvedAt by TriggerSentTable.resolvedAt
    var openIncidentKey by TriggerSentTable.openIncidentKey
    var createdAt by TriggerSentTable.createdAt
    var updatedAt by TriggerSentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TriggerSentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
