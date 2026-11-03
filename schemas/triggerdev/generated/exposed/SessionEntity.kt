package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionEntity>(SessionTable)

    var friendlyId by SessionTable.friendlyId
    var externalId by SessionTable.externalId
    var type by SessionTable.type
    var projectId by SessionTable.projectId
    var runtimeEnvironmentId by SessionTable.runtimeEnvironmentId
    var environmentType by SessionTable.environmentType
    var organizationId by SessionTable.organizationId
    var taskIdentifier by SessionTable.taskIdentifier
    var triggerConfig by SessionTable.triggerConfig
    var isTest by SessionTable.isTest
    var tags by SessionTable.tags
    var metadata by SessionTable.metadata
    var currentRunId by SessionTable.currentRunId
    var currentRunVersion by SessionTable.currentRunVersion
    var closedAt by SessionTable.closedAt
    var closedReason by SessionTable.closedReason
    var expiresAt by SessionTable.expiresAt
    var createdAt by SessionTable.createdAt
    var updatedAt by SessionTable.updatedAt
    var streamBasinName by SessionTable.streamBasinName
    var chatSnapshotStoragePath by SessionTable.chatSnapshotStoragePath
    val runs by SessionRunEntity referrersOn SessionRunTable.sessionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SessionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
