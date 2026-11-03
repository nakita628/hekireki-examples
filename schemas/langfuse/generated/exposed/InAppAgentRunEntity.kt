package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class InAppAgentRunEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<InAppAgentRunEntity>(InAppAgentRunTable)

    val id1 by InAppAgentRunTable.id1
    val projectId by InAppAgentRunTable.projectId
    val project by ProjectEntity referencedOn InAppAgentRunTable.projectId
    var conversationId by InAppAgentRunTable.conversationId
    val conversation by InAppAgentConversationEntity referencedOn InAppAgentRunTable
    var triggeredByUserId by InAppAgentRunTable.triggeredByUserId
    var triggeredByUser by UserEntity optionalReferencedOn InAppAgentRunTable.triggeredByUserId
    var model by InAppAgentRunTable.model
    var mcpApiKeyId by InAppAgentRunTable.mcpApiKeyId
    var errorCode by InAppAgentRunTable.errorCode
    var errorMessage by InAppAgentRunTable.errorMessage
    var finishedAt by InAppAgentRunTable.finishedAt
    var status by InAppAgentRunTable.status
    var request by InAppAgentRunTable.request
    var claimedAt by InAppAgentRunTable.claimedAt
    var heartbeatAt by InAppAgentRunTable.heartbeatAt
    var cancelRequestedAt by InAppAgentRunTable.cancelRequestedAt
    var createdAt by InAppAgentRunTable.createdAt
    var updatedAt by InAppAgentRunTable.updatedAt
    val events by InAppAgentEventEntity referrersOn InAppAgentEventTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InAppAgentRunTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
