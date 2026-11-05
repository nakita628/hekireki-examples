package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ReportTable : IdTable<UUID>("reports") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val name = text("name")
    val interval = pgEnum("interval", "\"Interval\"", Interval.entries, Interval::dbName)
    val range = text("range").default("30d")
    val chartType = pgEnum("chartType", "\"ChartType\"", ChartType.entries, ChartType::dbName)
    val lineType = text("lineType").default("monotone")
    val breakdowns = jsonb<String>("breakdowns", { it }, { it })
    val events = jsonb<String>("events", { it }, { it })
    val globalFilters = jsonb<String>("globalFilters", { it }, { it }).default("[]")
    val formula = text("formula").nullable()
    val unit = text("unit").nullable()
    val metric = pgEnum("metric", "\"Metric\"", Metric.entries, Metric::dbName).default(Metric.SUM)
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"reports_projectId_fkey\"",
        )
    val previous = bool("previous").default(false)
    val criteria = text("criteria").nullable()
    val funnelGroup = text("funnelGroup").nullable()
    val funnelWindow = double("funnelWindow").nullable()
    val options1 = jsonb<String>("options", { it }, { it }).nullable()
    val display = jsonb<String>("display", { it }, { it }).nullable()
    val visibleSeries = array("visibleSeries", TextColumnType()).nullable()
    val startDate = text("startDate").nullable()
    val endDate = text("endDate").nullable()
    val dashboardId =
        reference(
            "dashboardId",
            DashboardTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"reports_dashboardId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "reports_pkey")
}
