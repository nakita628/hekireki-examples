package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScenarioVersionTable : IdTable<String>("\"ScenarioVersion\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val scenarioId = text("scenarioId")
    val projectId = text("projectId")
    val version = integer("version")
    val authorId = text("authorId").nullable()
    val authorLabel = text("authorLabel")
    val changeDescription = text("changeDescription").nullable()
    val snapshot = jsonb<String>("snapshot", { it }, { it })
    val schemaVersion = integer("schemaVersion").default(1)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ScenarioVersion_pkey\"")

    init {
        index("\"ScenarioVersion_projectId_scenarioId_version_idx\"", false, projectId, scenarioId, version)
        index("\"ScenarioVersion_createdAt_idx\"", false, createdAt)
        uniqueIndex("\"ScenarioVersion_scenarioId_version_key\"", scenarioId, version)
    }
}
