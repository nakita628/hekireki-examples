package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AgentTable : IdTable<String>("\"Agent\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val type = text("type")
    val config = jsonb<String>("config", { it }, { it })
    val workflowId = text("workflowId").nullable()
    val copiedFromAgentId = text("copiedFromAgentId").nullable()
    val environment = text("environment").nullable()
    val ownerUserId = text("ownerUserId").nullable()
    val hostLabel = text("hostLabel").nullable()
    val identityKey = text("identityKey").nullable()
    val lastSeenAt = pgTimestamp("lastSeenAt", 3).nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Agent_pkey\"")

    init {
        index("\"Agent_projectId_idx\"", false, projectId)
        index("\"Agent_workflowId_idx\"", false, workflowId)
        index("\"Agent_copiedFromAgentId_idx\"", false, copiedFromAgentId)
        index("\"Agent_projectId_lastSeenAt_idx\"", false, projectId, lastSeenAt)
        uniqueIndex("\"Agent_projectId_identityKey_key\"", projectId, identityKey)
    }
}
