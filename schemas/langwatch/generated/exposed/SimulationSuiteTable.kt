package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SimulationSuiteTable : IdTable<String>("\"SimulationSuite\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val slug = text("slug")
    val description = text("description").nullable()
    val scenarioIds = array("scenarioIds", TextColumnType()).nullable()
    val targets = jsonb<String>("targets", { it }, { it })
    val repeatCount = integer("repeatCount").default(1)
    val labels = array("labels", TextColumnType()).nullable()
    val simulatorModel = text("simulatorModel").nullable()
    val judgeModel = text("judgeModel").nullable()
    val kind = text("kind").default("run_plan")
    val scope = jsonb<String>("scope", { it }, { it }).nullable()
    val fields1 = jsonb<String>("fields", { it }, { it }).nullable()
    val evaluators = jsonb<String>("evaluators", { it }, { it }).nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SimulationSuite_pkey\"")

    init {
        index("\"SimulationSuite_projectId_idx\"", false, projectId)
        index("\"SimulationSuite_projectId_kind_idx\"", false, projectId, kind)
        uniqueIndex("\"SimulationSuite_projectId_slug_key\"", projectId, slug)
    }
}
