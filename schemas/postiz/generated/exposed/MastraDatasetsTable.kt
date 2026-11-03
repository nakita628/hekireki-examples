package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraDatasetsTable : IdTable<String>("mastra_datasets") {
    override val id = text("id").entityId()
    val name = text("name")
    val description = text("description").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val inputSchema = jsonb<String>("inputSchema", { it }, { it }).nullable()
    val groundTruthSchema = jsonb<String>("groundTruthSchema", { it }, { it }).nullable()
    val requestContextSchema = jsonb<String>("requestContextSchema", { it }, { it }).nullable()
    val tags = jsonb<String>("tags", { it }, { it }).nullable()
    val targetType = text("targetType").nullable()
    val targetIds = jsonb<String>("targetIds", { it }, { it }).nullable()
    val scorerIds = jsonb<String>("scorerIds", { it }, { it }).nullable()
    val version = integer("version")
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId = text("organizationId").nullable()
    val projectId = text("projectId").nullable()
    val candidateKey = text("candidateKey").nullable()
    val candidateId = text("candidateId").nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_datasets_pkey")

    init {
        index("idx_datasets_candidate", false, candidateKey, candidateId)
        index("idx_datasets_org_project", false, organizationId, projectId)
    }
}
