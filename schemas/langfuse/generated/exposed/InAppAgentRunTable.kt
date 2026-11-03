package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object InAppAgentRunTable : CompositeIdTable("in_app_agent_runs") {
    val id1 = text("id").entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "in_app_agent_runs_project_id_fkey",
        )
    val conversationId = text("conversation_id")
    val triggeredByUserId =
        optReference(
            "triggered_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "in_app_agent_runs_triggered_by_user_id_fkey",
        )
    val model = text("model").nullable()
    val mcpApiKeyId = text("mcp_api_key_id").nullable()
    val errorCode = text("error_code").nullable()
    val errorMessage = text("error_message").nullable()
    val finishedAt = pgTimestamp("finished_at", 3).nullable()
    val status = text("status").nullable()
    val request = jsonb<String>("request", { it }, { it }).nullable()
    val claimedAt = pgTimestamp("claimed_at", 3).nullable()
    val heartbeatAt = pgTimestamp("heartbeat_at", 3).nullable()
    val cancelRequestedAt = pgTimestamp("cancel_requested_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id1, projectId, name = "in_app_agent_runs_pkey")

    init {
        addIdColumn(projectId)
        index("in_app_agent_runs_project_conversation_created_idx", false, projectId, conversationId, createdAt)
        foreignKey(
            conversationId to InAppAgentConversationTable.id1,
            projectId to InAppAgentConversationTable.projectId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "in_app_agent_runs_conversation_id_project_id_fkey",
        )
    }
}
