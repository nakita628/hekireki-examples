package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DashboardWidgetTable : IdTable<String>("\"DashboardWidget\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val dashboardId =
        reference(
            "dashboardId",
            DashboardTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DashboardWidget_dashboardId_fkey\"",
        )
    val chartId =
        reference(
            "chartId",
            ChartTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DashboardWidget_chartId_fkey\"",
        )
    val layout = jsonb<String>("layout", { it }, { it }).default("{\"x\":0,\"y\":0,\"w\":4,\"h\":3}")
    val order = integer("order").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"DashboardWidget_pkey\"")

    init {
        index("\"DashboardWidget_dashboardId_order_idx\"", false, dashboardId, order)
        uniqueIndex("\"DashboardWidget_dashboardId_chartId_key\"", dashboardId, chartId)
    }
}
