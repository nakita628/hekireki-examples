package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraExperimentResultsTable : IdTable<String>("mastra_experiment_results") {
    override val id = text("id").entityId()
    val experimentId = text("experimentId")
    val itemId = text("itemId")
    val itemDatasetVersion = integer("itemDatasetVersion").nullable()
    val input = jsonb<String>("input", { it }, { it })
    val output = jsonb<String>("output", { it }, { it }).nullable()
    val groundTruth = jsonb<String>("groundTruth", { it }, { it }).nullable()
    val error = jsonb<String>("error", { it }, { it }).nullable()
    val startedAt = pgTimestamp("startedAt", 6)
    val completedAt = pgTimestamp("completedAt", 6)
    val retryCount = integer("retryCount")
    val traceId = text("traceId").nullable()
    val status = text("status").nullable()
    val tags = jsonb<String>("tags", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val startedAtZ =
        pgTimestamptz("startedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAtZ =
        pgTimestamptz("completedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val comment = text("comment").nullable()
    val toolMockReport = jsonb<String>("toolMockReport", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val organizationId = text("organizationId").nullable()
    val projectId = text("projectId").nullable()
    val attempt = integer("attempt").nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_experiment_results_pkey")

    init {
        index("idx_experiment_results_experimentid", false, experimentId)
        index("idx_experiment_results_org_project", false, organizationId, projectId)
        index("idx_experiment_results_tags_gin", false, tags, indexType = "gin")
        uniqueIndex("idx_experiment_results_exp_item_attempt", experimentId, itemId, attempt)
    }
}
