package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskQueueEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskQueueEntity>(TaskQueueTable)

    var friendlyId by TaskQueueTable.friendlyId
    var name by TaskQueueTable.name
    var type by TaskQueueTable.type
    var version by TaskQueueTable.version
    var orderableName by TaskQueueTable.orderableName
    var concurrencyVersion by TaskQueueTable.concurrencyVersion
    var role by TaskQueueTable.role
    var project by ProjectEntity referencedOn TaskQueueTable.projectId
    var projectId by TaskQueueTable.projectId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn TaskQueueTable.runtimeEnvironmentId
    var runtimeEnvironmentId by TaskQueueTable.runtimeEnvironmentId
    var concurrencyLimit by TaskQueueTable.concurrencyLimit
    var concurrencyLimitOverriddenAt by TaskQueueTable.concurrencyLimitOverriddenAt
    var concurrencyLimitOverriddenBy by TaskQueueTable.concurrencyLimitOverriddenBy
    var concurrencyLimitBase by TaskQueueTable.concurrencyLimitBase
    var concurrencyLimitOverridePercent by TaskQueueTable.concurrencyLimitOverridePercent
    var totalConcurrencyLimit by TaskQueueTable.totalConcurrencyLimit
    var totalConcurrencyLimitOverriddenAt by TaskQueueTable.totalConcurrencyLimitOverriddenAt
    var totalConcurrencyLimitOverriddenBy by TaskQueueTable.totalConcurrencyLimitOverriddenBy
    var totalConcurrencyLimitBase by TaskQueueTable.totalConcurrencyLimitBase
    var rateLimit by TaskQueueTable.rateLimit
    var paused by TaskQueueTable.paused
    var archivedAt by TaskQueueTable.archivedAt
    var createdAt by TaskQueueTable.createdAt
    var updatedAt by TaskQueueTable.updatedAt
    val attempts by TaskRunAttemptEntity referrersOn TaskRunAttemptTable.queueId
    val tasks by BackgroundWorkerTaskEntity optionalReferrersOn BackgroundWorkerTaskTable.queueId
    var workers by BackgroundWorkerEntity via BackgroundWorkerToTaskQueueTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskQueueTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
