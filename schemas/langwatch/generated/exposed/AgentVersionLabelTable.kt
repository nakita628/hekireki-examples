package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AgentVersionLabelTable : IdTable<String>("\"AgentVersionLabel\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val agentId = text("agentId")
    val label = text("label")
    val versionId = text("versionId")
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AgentVersionLabel_pkey\"")

    init {
        index("\"AgentVersionLabel_projectId_idx\"", false, projectId)
        index("\"AgentVersionLabel_versionId_idx\"", false, versionId)
        uniqueIndex("\"AgentVersionLabel_agentId_label_key\"", agentId, label)
    }
}
