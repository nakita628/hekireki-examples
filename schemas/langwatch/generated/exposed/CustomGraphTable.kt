package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object CustomGraphTable : IdTable<String>("\"CustomGraph\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val graph = jsonb<String>("graph", { it }, { it })
    val filters = jsonb<String>("filters", { it }, { it }).nullable()
    val kind = text("kind").default("builder")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val dashboardId = text("dashboardId").nullable()
    val gridColumn = integer("gridColumn").default(0)
    val gridRow = integer("gridRow").default(0)
    val colSpan = integer("colSpan").default(1)
    val rowSpan = integer("rowSpan").default(1)
    override val primaryKey = PrimaryKey(id, name = "\"CustomGraph_pkey\"")

    init {
        index("\"CustomGraph_projectId_idx\"", false, projectId)
        index("\"CustomGraph_dashboardId_idx\"", false, dashboardId)
        index("\"CustomGraph_projectId_kind_idx\"", false, projectId, kind)
    }
}
