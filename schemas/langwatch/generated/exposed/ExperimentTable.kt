package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExperimentTable : IdTable<String>("\"Experiment\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name").nullable()
    val type = pgEnum("type", "\"ExperimentType\"", ExperimentType.entries, ExperimentType::dbName)
    val slug = text("slug")
    val projectId = text("projectId")
    val workflowId = text("workflowId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val workbenchState = jsonb<String>("workbenchState", { it }, { it }).nullable()
    val workbenchVersion = integer("workbenchVersion").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"Experiment_pkey\"")

    init {
        index("\"Experiment_projectId_idx\"", false, projectId)
        index("\"Experiment_workflowId_idx\"", false, workflowId)
        index("\"Experiment_projectId_archivedAt_idx\"", false, projectId, archivedAt)
        uniqueIndex("\"Experiment_projectId_slug_key\"", projectId, slug)
    }
}
