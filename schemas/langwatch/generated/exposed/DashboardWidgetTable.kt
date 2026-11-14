package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DashboardWidgetTable : IdTable<String>("\"DashboardWidget\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val dashboardId = text("dashboardId").nullable()
    val name = text("name")
    val definition = jsonb<String>("definition", { it }, { it })
    val gridColumn = integer("gridColumn").default(0)
    val gridRow = integer("gridRow").default(0)
    val colSpan = integer("colSpan").default(1)
    val rowSpan = integer("rowSpan").default(1)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DashboardWidget_pkey\"")

    init {
        index("\"DashboardWidget_projectId_dashboardId_idx\"", false, projectId, dashboardId)
    }
}
