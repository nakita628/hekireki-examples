package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class InAppAgentPendingToolApprovalEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<InAppAgentPendingToolApprovalEntity>(InAppAgentPendingToolApprovalTable)

    val projectId by InAppAgentPendingToolApprovalTable.projectId
    val project by ProjectEntity referencedOn InAppAgentPendingToolApprovalTable.projectId
    val conversationId by InAppAgentPendingToolApprovalTable.conversationId
    val conversation by InAppAgentConversationEntity referencedOn InAppAgentPendingToolApprovalTable
    val toolCallId by InAppAgentPendingToolApprovalTable.toolCallId
    var approvalFingerprint by InAppAgentPendingToolApprovalTable.approvalFingerprint
    var expiresAt by InAppAgentPendingToolApprovalTable.expiresAt
    var createdAt by InAppAgentPendingToolApprovalTable.createdAt
    var updatedAt by InAppAgentPendingToolApprovalTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InAppAgentPendingToolApprovalTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
