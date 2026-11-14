package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AgentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AgentEntity>(AgentTable)

    var projectId by AgentTable.projectId
    var name by AgentTable.name
    var type by AgentTable.type
    var config by AgentTable.config
    var workflowId by AgentTable.workflowId
    var copiedFromAgentId by AgentTable.copiedFromAgentId
    var environment by AgentTable.environment
    var ownerUserId by AgentTable.ownerUserId
    var hostLabel by AgentTable.hostLabel
    var identityKey by AgentTable.identityKey
    var lastSeenAt by AgentTable.lastSeenAt
    var archivedAt by AgentTable.archivedAt
    var createdAt by AgentTable.createdAt
    var updatedAt by AgentTable.updatedAt
    var createdById by AgentTable.createdById
    var updatedById by AgentTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AgentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
