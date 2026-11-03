package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraBackgroundTasksEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraBackgroundTasksEntity>(MastraBackgroundTasksTable)

    var toolCallId by MastraBackgroundTasksTable.toolCallId
    var toolName by MastraBackgroundTasksTable.toolName
    var agentId by MastraBackgroundTasksTable.agentId
    var runId by MastraBackgroundTasksTable.runId
    var threadId by MastraBackgroundTasksTable.threadId
    var resourceId by MastraBackgroundTasksTable.resourceId
    var status by MastraBackgroundTasksTable.status
    var args by MastraBackgroundTasksTable.args
    var result by MastraBackgroundTasksTable.result
    var error by MastraBackgroundTasksTable.error
    var suspendPayload by MastraBackgroundTasksTable.suspendPayload
    var retryCount by MastraBackgroundTasksTable.retryCount
    var maxRetries by MastraBackgroundTasksTable.maxRetries
    var timeoutMs by MastraBackgroundTasksTable.timeoutMs
    var createdAt by MastraBackgroundTasksTable.createdAt
    var startedAt by MastraBackgroundTasksTable.startedAt
    var suspendedAt by MastraBackgroundTasksTable.suspendedAt
    var completedAt by MastraBackgroundTasksTable.completedAt
    var createdAtZ by MastraBackgroundTasksTable.createdAtZ
    var startedAtZ by MastraBackgroundTasksTable.startedAtZ
    var suspendedAtZ by MastraBackgroundTasksTable.suspendedAtZ
    var completedAtZ by MastraBackgroundTasksTable.completedAtZ
}
