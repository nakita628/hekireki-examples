package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class InAppAgentConversationEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<InAppAgentConversationEntity>(InAppAgentConversationTable)

    val id1 by InAppAgentConversationTable.id1
    val projectId by InAppAgentConversationTable.projectId
    val project by ProjectEntity referencedOn InAppAgentConversationTable.projectId
    var createdByUserId by InAppAgentConversationTable.createdByUserId
    var createdByUser by UserEntity optionalReferencedOn InAppAgentConversationTable.createdByUserId
    var title by InAppAgentConversationTable.title
    var renamedByUserAt by InAppAgentConversationTable.renamedByUserAt
    var visibilityScope by InAppAgentConversationTable.visibilityScope
    var providerSessionId by InAppAgentConversationTable.providerSessionId
    var alwaysAllowedTools by InAppAgentConversationTable.alwaysAllowedTools
    var deletedAt by InAppAgentConversationTable.deletedAt
    var createdAt by InAppAgentConversationTable.createdAt
    var updatedAt by InAppAgentConversationTable.updatedAt
    val events by InAppAgentEventEntity referrersOn InAppAgentEventTable
    val runs by InAppAgentRunEntity referrersOn InAppAgentRunTable
    val pendingToolApprovals by InAppAgentPendingToolApprovalEntity referrersOn InAppAgentPendingToolApprovalTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InAppAgentConversationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
