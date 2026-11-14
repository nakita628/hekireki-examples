package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LlmPromptConfigTable : IdTable<String>("\"LlmPromptConfig\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val handle = text("handle").nullable().uniqueIndex("\"LlmPromptConfig_handle_key\"")
    val name = text("name")
    val projectId = text("projectId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val organizationId = text("organizationId")
    val scope = pgEnum("scope", "\"PromptScope\"", PromptScope.entries, PromptScope::dbName).default(PromptScope.PROJECT)
    val copiedFromPromptId = text("copiedFromPromptId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LlmPromptConfig_pkey\"")

    init {
        index("\"LlmPromptConfig_projectId_idx\"", false, projectId)
        index("\"LlmPromptConfig_organizationId_scope_idx\"", false, organizationId, scope)
        index("\"LlmPromptConfig_copiedFromPromptId_idx\"", false, copiedFromPromptId)
    }
}
