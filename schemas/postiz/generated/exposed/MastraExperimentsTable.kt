package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraExperimentsTable : IdTable<String>("mastra_experiments") {
    override val id = text("id").entityId()
    val name = text("name").nullable()
    val description = text("description").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val datasetId = text("datasetId").nullable()
    val datasetVersion = integer("datasetVersion").nullable()
    val targetType = text("targetType")
    val targetId = text("targetId")
    val status = text("status")
    val totalItems = integer("totalItems")
    val succeededCount = integer("succeededCount")
    val failedCount = integer("failedCount")
    val skippedCount = integer("skippedCount")
    val startedAt = pgTimestamp("startedAt", 6).nullable()
    val completedAt = pgTimestamp("completedAt", 6).nullable()
    val agentVersion = text("agentVersion").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val startedAtZ =
        pgTimestamptz("startedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAtZ =
        pgTimestamptz("completedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId = text("organizationId").nullable()
    val projectId = text("projectId").nullable()
    val provenance = jsonb<String>("provenance", { it }, { it }).nullable()
    val runnerAttestation = jsonb<String>("runnerAttestation", { it }, { it }).nullable()
    val experimentSetId = text("experimentSetId").nullable()
    val comparisonId = text("comparisonId").nullable()
    val variantId = text("variantId").nullable()
    val trialIndex = integer("trialIndex").nullable()
    val scorerIds = jsonb<String>("scorerIds", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_experiments_pkey")

    init {
        index("idx_experiments_datasetid", false, datasetId)
        index("idx_experiments_grouping", false, experimentSetId, comparisonId, variantId, trialIndex)
        index("idx_experiments_org_project", false, organizationId, projectId)
    }
}
