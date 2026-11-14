package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MonitorTable : IdTable<String>("\"Monitor\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val experimentId = text("experimentId").nullable().uniqueIndex("\"Monitor_experimentId_key\"")
    val evaluatorId = text("evaluatorId").nullable()
    val checkType = text("checkType")
    val name = text("name")
    val slug = text("slug")
    val executionMode =
        pgEnum("executionMode", "\"EvaluationExecutionMode\"", EvaluationExecutionMode.entries, EvaluationExecutionMode::dbName).default(
            EvaluationExecutionMode.ON_MESSAGE,
        )
    val enabled = bool("enabled").default(true)
    val preconditions = jsonb<String>("preconditions", { it }, { it })
    val parameters = jsonb<String>("parameters", { it }, { it })
    val mappings = jsonb<String>("mappings", { it }, { it }).nullable()
    val sample = double("sample").default(1.0)
    val level = text("level").default("trace")
    val threadIdleTimeout = integer("threadIdleTimeout").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val autoPausedAt = pgTimestamp("autoPausedAt", 3).nullable()
    val autoPausedReason = text("autoPausedReason").nullable()
    val evaluatorLabel = text("evaluatorLabel").nullable()
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Monitor_pkey\"")

    init {
        index("\"Monitor_projectId_idx\"", false, projectId)
        index("\"Monitor_evaluatorId_idx\"", false, evaluatorId)
        uniqueIndex("\"Monitor_projectId_slug_key\"", projectId, slug)
    }
}
