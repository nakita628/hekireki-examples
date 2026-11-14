package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AgentVersionTable : IdTable<String>("\"AgentVersion\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val agentId = text("agentId")
    val version = integer("version")
    val name = text("name")
    val type = text("type")
    val config = jsonb<String>("config", { it }, { it })
    val workflowId = text("workflowId").nullable()
    val commitMessage = text("commitMessage").nullable()
    val authorId = text("authorId").nullable()
    val authorKind = text("authorKind").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AgentVersion_pkey\"")

    init {
        index("\"AgentVersion_projectId_idx\"", false, projectId)
        uniqueIndex("\"AgentVersion_agentId_version_key\"", agentId, version)
    }
}
