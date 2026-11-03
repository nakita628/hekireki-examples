package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraScorersTable : IdTable<String>("mastra_scorers") {
    override val id = text("id").entityId()
    val scorerId = text("scorerId")
    val traceId = text("traceId").nullable()
    val runId = text("runId")
    val scorer = jsonb<String>("scorer", { it }, { it })
    val preprocessStepResult = jsonb<String>("preprocessStepResult", { it }, { it }).nullable()
    val extractStepResult = jsonb<String>("extractStepResult", { it }, { it }).nullable()
    val analyzeStepResult = jsonb<String>("analyzeStepResult", { it }, { it }).nullable()
    val score = double("score")
    val reason = text("reason").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val preprocessPrompt = text("preprocessPrompt").nullable()
    val extractPrompt = text("extractPrompt").nullable()
    val generateScorePrompt = text("generateScorePrompt").nullable()
    val generateReasonPrompt = text("generateReasonPrompt").nullable()
    val analyzePrompt = text("analyzePrompt").nullable()
    val reasonPrompt = text("reasonPrompt").nullable()
    val input = jsonb<String>("input", { it }, { it })
    val output = jsonb<String>("output", { it }, { it })
    val additionalContext = jsonb<String>("additionalContext", { it }, { it }).nullable()
    val runtimeContext = jsonb<String>("runtimeContext", { it }, { it }).nullable()
    val entityType = text("entityType").nullable()
    val entity = jsonb<String>("entity", { it }, { it }).nullable()
    val entityId = text("entityId").nullable()
    val source1 = text("source")
    val resourceId = text("resourceId").nullable()
    val threadId = text("threadId").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val spanId = text("spanId").nullable()
    val requestContext = jsonb<String>("requestContext", { it }, { it }).nullable()
    val organizationId = text("organizationId").nullable()
    val projectId = text("projectId").nullable()
    val batchId = text("batchId").nullable()
    val datasetId = text("datasetId").nullable()
    val datasetItemId = text("datasetItemId").nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_scorers_pkey")

    init {
        index(
            "public_mastra_scores_trace_id_span_id_created_at_idx",
            false,
            functions = listOf(indexExpression("\"traceId\""), indexExpression("\"spanId\""), indexExpression("\"createdAt\" DESC")),
        )
        index(
            "mastra_scores_trace_id_span_id_created_at_idx",
            false,
            functions = listOf(indexExpression("\"traceId\""), indexExpression("\"spanId\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
