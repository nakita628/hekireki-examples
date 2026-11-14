package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExperimentVersionTable : IdTable<String>("\"ExperimentVersion\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val experimentId = text("experimentId")
    val projectId = text("projectId")
    val version = integer("version")
    val counterVersion = integer("counterVersion")
    val autoSaved = bool("autoSaved").default(false)
    val commitMessage = text("commitMessage").nullable()
    val authorId = text("authorId").nullable()
    val authorLabel = text("authorLabel")
    val runId = text("runId").nullable()
    val state = jsonb<String>("state", { it }, { it })
    val schemaVersion = text("schemaVersion").default("1")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ExperimentVersion_pkey\"")

    init {
        index("\"ExperimentVersion_projectId_experimentId_autoSaved_idx\"", false, projectId, experimentId, autoSaved)
        index("\"ExperimentVersion_projectId_experimentId_version_idx\"", false, projectId, experimentId, version)
        index("\"ExperimentVersion_projectId_experimentId_counterVersion_idx\"", false, projectId, experimentId, counterVersion)
        index("\"ExperimentVersion_createdAt_idx\"", false, createdAt)
        uniqueIndex("\"ExperimentVersion_experimentId_version_key\"", experimentId, version)
    }
}
