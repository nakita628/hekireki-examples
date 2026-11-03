package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InAppAgentPendingToolApprovalTable : CompositeIdTable("in_app_agent_pending_tool_approvals") {
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "in_app_agent_pending_tool_approvals_project_id_fkey",
        )
    val conversationId = text("conversation_id").entityId()
    val toolCallId = text("tool_call_id").entityId()
    val approvalFingerprint = text("approval_fingerprint")
    val expiresAt = pgTimestamp("expires_at", 3)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(projectId, conversationId, toolCallId, name = "in_app_agent_pending_tool_approvals_pkey")

    init {
        addIdColumn(projectId)
        index("in_app_agent_pending_tool_approvals_expires_at_idx", false, expiresAt)
        foreignKey(
            conversationId to InAppAgentConversationTable.id1,
            projectId to InAppAgentConversationTable.projectId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "in_app_agent_pending_tool_approvals_conversation_id_projec_fkey",
        )
    }
}
