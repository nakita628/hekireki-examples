package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScenarioTable : IdTable<String>("\"Scenario\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val situation = text("situation")
    val criteria = array("criteria", TextColumnType()).nullable()
    val labels = array("labels", TextColumnType()).nullable()
    val parameters = jsonb<String>("parameters", { it }, { it }).nullable()
    val simulatorModel = text("simulatorModel").nullable()
    val judgeModel = text("judgeModel").nullable()
    val maxTurns = integer("maxTurns").nullable()
    val minTurns = integer("minTurns").nullable()
    val fields1 = jsonb<String>("fields", { it }, { it }).nullable()
    val callerVoice = jsonb<String>("callerVoice", { it }, { it }).nullable()
    val testSuiteId = text("testSuiteId").nullable()
    val version = integer("version").default(1)
    val lastUpdatedById = text("lastUpdatedById").nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Scenario_pkey\"")

    init {
        index("\"Scenario_projectId_idx\"", false, projectId)
        index("\"Scenario_projectId_testSuiteId_idx\"", false, projectId, testSuiteId)
    }
}
