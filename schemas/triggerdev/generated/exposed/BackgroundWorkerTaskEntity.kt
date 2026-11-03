package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BackgroundWorkerTaskEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BackgroundWorkerTaskEntity>(BackgroundWorkerTaskTable)

    var slug by BackgroundWorkerTaskTable.slug
    var description by BackgroundWorkerTaskTable.description
    var friendlyId by BackgroundWorkerTaskTable.friendlyId
    var filePath by BackgroundWorkerTaskTable.filePath
    var exportName by BackgroundWorkerTaskTable.exportName
    var worker by BackgroundWorkerEntity referencedOn BackgroundWorkerTaskTable.workerId
    var workerId by BackgroundWorkerTaskTable.workerId
    var project by ProjectEntity referencedOn BackgroundWorkerTaskTable.projectId
    var projectId by BackgroundWorkerTaskTable.projectId
    var file by BackgroundWorkerFileEntity optionalReferencedOn BackgroundWorkerTaskTable.fileId
    var fileId by BackgroundWorkerTaskTable.fileId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn BackgroundWorkerTaskTable.runtimeEnvironmentId
    var runtimeEnvironmentId by BackgroundWorkerTaskTable.runtimeEnvironmentId
    var createdAt by BackgroundWorkerTaskTable.createdAt
    var updatedAt by BackgroundWorkerTaskTable.updatedAt
    val attempts by TaskRunAttemptEntity referrersOn TaskRunAttemptTable.backgroundWorkerTaskId
    val runs by TaskRunEntity optionalReferrersOn TaskRunTable.lockedById
    var queueConfig by BackgroundWorkerTaskTable.queueConfig
    var retryConfig by BackgroundWorkerTaskTable.retryConfig
    var machineConfig by BackgroundWorkerTaskTable.machineConfig
    var gates by BackgroundWorkerTaskTable.gates
    var queueId by BackgroundWorkerTaskTable.queueId
    var queue by TaskQueueEntity optionalReferencedOn BackgroundWorkerTaskTable.queueId
    var maxDurationInSeconds by BackgroundWorkerTaskTable.maxDurationInSeconds
    var ttl by BackgroundWorkerTaskTable.ttl
    var triggerSource by BackgroundWorkerTaskTable.triggerSource
    var config by BackgroundWorkerTaskTable.config
    var payloadSchema by BackgroundWorkerTaskTable.payloadSchema

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BackgroundWorkerTaskTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
