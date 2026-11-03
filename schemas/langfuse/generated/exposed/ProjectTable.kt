package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectTable : IdTable<String>("projects") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val orgId =
        reference(
            "org_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "projects_org_id_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deleted_at", 3).nullable()
    val name = text("name")
    val retentionDays = integer("retention_days").nullable()
    val hasTraces = bool("has_traces").default(false)
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val homeDashboardId =
        optReference(
            "home_dashboard_id",
            DashboardTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "projects_home_dashboard_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "projects_pkey")

    init {
        index("projects_org_id_idx", false, orgId)
    }
}
