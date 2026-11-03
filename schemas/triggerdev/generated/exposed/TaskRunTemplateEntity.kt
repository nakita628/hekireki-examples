package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskRunTemplateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunTemplateEntity>(TaskRunTemplateTable)

    var taskSlug by TaskRunTemplateTable.taskSlug
    var triggerSource by TaskRunTemplateTable.triggerSource
    var label by TaskRunTemplateTable.label
    var payload by TaskRunTemplateTable.payload
    var payloadType by TaskRunTemplateTable.payloadType
    var metadata by TaskRunTemplateTable.metadata
    var metadataType by TaskRunTemplateTable.metadataType
    var queue by TaskRunTemplateTable.queue
    var concurrencyKey by TaskRunTemplateTable.concurrencyKey
    var ttlSeconds by TaskRunTemplateTable.ttlSeconds
    var maxAttempts by TaskRunTemplateTable.maxAttempts
    var maxDurationSeconds by TaskRunTemplateTable.maxDurationSeconds
    var tags by TaskRunTemplateTable.tags
    var machinePreset by TaskRunTemplateTable.machinePreset
    var projectId by TaskRunTemplateTable.projectId
    var organizationId by TaskRunTemplateTable.organizationId
    var createdAt by TaskRunTemplateTable.createdAt
    var updatedAt by TaskRunTemplateTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunTemplateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
