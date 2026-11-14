package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BatchEvaluationTable : IdTable<String>("\"BatchEvaluation\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val experimentId = text("experimentId")
    val projectId = text("projectId")
    val data = jsonb<String>("data", { it }, { it }).default("{}")
    val status = text("status")
    val score = double("score")
    val label = text("label").nullable()
    val passed = bool("passed")
    val details = text("details")
    val cost = double("cost")
    val datasetSlug = text("datasetSlug")
    val datasetId = text("datasetId")
    val evaluation = text("evaluation")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"BatchEvaluation_pkey\"")

    init {
        index("\"BatchEvaluation_projectId_idx\"", false, projectId)
        index("\"BatchEvaluation_datasetId_idx\"", false, datasetId)
        index("\"BatchEvaluation_experimentId_idx\"", false, experimentId)
        uniqueIndex("\"BatchEvaluation_id_key\"", id)
    }
}
