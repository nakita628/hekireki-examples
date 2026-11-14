package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EvaluatorTable : IdTable<String>("\"Evaluator\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val slug = text("slug").nullable()
    val type = text("type")
    val config = jsonb<String>("config", { it }, { it })
    val workflowId = text("workflowId").nullable()
    val copiedFromEvaluatorId = text("copiedFromEvaluatorId").nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Evaluator_pkey\"")

    init {
        index("\"Evaluator_projectId_idx\"", false, projectId)
        index("\"Evaluator_workflowId_idx\"", false, workflowId)
        index("\"Evaluator_copiedFromEvaluatorId_idx\"", false, copiedFromEvaluatorId)
        uniqueIndex("\"Evaluator_projectId_slug_key\"", projectId, slug)
    }
}
