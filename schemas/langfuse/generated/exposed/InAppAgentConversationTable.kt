package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InAppAgentConversationTable : CompositeIdTable("in_app_agent_conversations") {
    val id1 = text("id").entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "in_app_agent_conversations_project_id_fkey",
        )
    val createdByUserId =
        optReference(
            "created_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "in_app_agent_conversations_created_by_user_id_fkey",
        )
    val title = text("title").nullable()
    val renamedByUserAt = pgTimestamp("renamed_by_user_at", 3).nullable()
    val visibilityScope =
        pgEnum(
            "visibility_scope",
            "\"InAppAgentConversationVisibilityScope\"",
            InAppAgentConversationVisibilityScope.entries,
            InAppAgentConversationVisibilityScope::dbName,
        ).default(InAppAgentConversationVisibilityScope.PERSONAL)
    val providerSessionId = text("provider_session_id").nullable()
    val alwaysAllowedTools = array("always_allowed_tools", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val deletedAt = pgTimestamp("deleted_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id1, projectId, name = "in_app_agent_conversations_pkey")

    init {
        addIdColumn(projectId)
        index("in_app_agent_conversations_project_user_list_idx", false, projectId, createdByUserId, deletedAt, updatedAt, id1)
        index("in_app_agent_conversations_project_updated_at_idx", false, projectId, updatedAt)
    }
}
