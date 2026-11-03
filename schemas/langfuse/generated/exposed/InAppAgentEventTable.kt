package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object InAppAgentEventTable : CompositeIdTable("in_app_agent_events") {
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "in_app_agent_events_project_id_fkey",
        )
    val conversationId = text("conversation_id").entityId()
    val runId = text("run_id")
    val sequenceNumber = integer("sequence_number").entityId()
    val type = text("type")
    val event = jsonb<String>("event", { it }, { it })
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(projectId, conversationId, sequenceNumber, name = "in_app_agent_events_pkey")

    init {
        addIdColumn(projectId)
        index("in_app_agent_events_project_run_idx", false, projectId, runId)
        foreignKey(
            conversationId to InAppAgentConversationTable.id1,
            projectId to InAppAgentConversationTable.projectId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "in_app_agent_events_conversation_id_project_id_fkey",
        )
        foreignKey(
            runId to InAppAgentRunTable.id1,
            projectId to InAppAgentRunTable.projectId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "in_app_agent_events_run_id_project_id_fkey",
        )
    }
}
