package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DashboardWidgetTable : IdTable<String>("dashboard_widgets") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy =
        optReference(
            "created_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "dashboard_widgets_created_by_fkey",
        )
    val updatedBy =
        optReference(
            "updated_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "dashboard_widgets_updated_by_fkey",
        )
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "dashboard_widgets_project_id_fkey",
        )
    val name = text("name")
    val description = text("description")
    val view = pgEnum("view", "\"DashboardWidgetViews\"", DashboardWidgetViews.entries, DashboardWidgetViews::dbName)
    val dimensions = jsonb<String>("dimensions", { it }, { it })
    val metrics = jsonb<String>("metrics", { it }, { it })
    val filters = jsonb<String>("filters", { it }, { it })
    val chartType = pgEnum("chart_type", "\"DashboardWidgetChartType\"", DashboardWidgetChartType.entries, DashboardWidgetChartType::dbName)
    val chartConfig = jsonb<String>("chart_config", { it }, { it })
    val minVersion = integer("min_version").default(1)
    override val primaryKey = PrimaryKey(id, name = "dashboard_widgets_pkey")
}
